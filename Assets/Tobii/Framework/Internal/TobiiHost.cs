//-----------------------------------------------------------------------
// Copyright 2016 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using System;
using System.Collections;
using Tobii.Framework;
using Tobii.Framework.Internal;
using UnityEngine;

namespace Assets.Tobii.Framework.Internal
{
    internal class TobiiHost : MonoBehaviour, ITobiiHost
    {
        private static bool _isConnected;
        private static TobiiHost _instance;
        private static bool _isShuttingDown;

        private static bool HasDisplayedEulaError;

        private GameViewBoundsProvider _gameViewBoundsProvider;
        private GameViewInfo _gameViewInfo = GameViewInfo.DefaultGameViewInfo;
        private GazeFocus _gazeFocus;

        private GazePointDataProvider _gazePointDataProvider;
        private HeadPoseDataProvider _headPoseDataProvider;
        private bool _updatedInFrame;


        public void Shutdown()
        {
            _isShuttingDown = true;

            Disconnect();
        }

        public DisplayInfo DisplayInfo
        {
            get
            {
                int displayHeight;
                int displayWidth;
                Interop.GetScreenSizeMm(out displayWidth, out displayHeight);
                return new DisplayInfo(displayWidth, displayHeight);
            }
        }

        public GameViewInfo GameViewInfo
        {
            get { return _gameViewInfo; }
        }

        public global::Tobii.Framework.UserPresence UserPresence
        {
            get
            {
                var presence = Interop.GetUserPresence();
                switch (presence)
                {
                    case global::Tobii.Framework.Internal.UserPresence.Away:
                        return global::Tobii.Framework.UserPresence.NotPresent;
                    case global::Tobii.Framework.Internal.UserPresence.Present:
                        return global::Tobii.Framework.UserPresence.Present;
                    default:
                        return global::Tobii.Framework.UserPresence.Unknown;
                }
            }
        }

        public bool IsInitialized
        {
            get { return _isConnected; }
        }

        public IGazeFocus GazeFocus
        {
            get { return _gazeFocus; }
        }

        public IDataProvider<global::Tobii.Framework.GazePoint> GetGazePointDataProvider()
        {
            SyncData();
            return _gazePointDataProvider;
        }

        public IDataProvider<global::Tobii.Framework.HeadPose> GetHeadPoseDataProvider()
        {
            SyncData();
            return _headPoseDataProvider;
        }

        //--------------------------------------------------------------------
        // Public Function and Properties
        //--------------------------------------------------------------------

        public static ITobiiHost GetInstance()
        {
            if (_isShuttingDown || !TobiiEulaFile.IsEulaAccepted())
            {
                if (!TobiiEulaFile.IsEulaAccepted() && !HasDisplayedEulaError)
                {
                    Debug.LogError(
                        "You need to accept EULA to be able to use Tobii Unity SDK.");
                    HasDisplayedEulaError = true;
                }

                return new TobiiHostStub();
            }

            if (_instance != null) return _instance;

            var newGameObject = new GameObject("TobiiHost");
            DontDestroyOnLoad(newGameObject);
            _instance = newGameObject.AddComponent<TobiiHost>();
            return _instance;
        }


        //--------------------------------------------------------------------
        // MonoBehaviour Messages
        //--------------------------------------------------------------------

        private void Awake()
        {
#if UNITY_EDITOR
            _gameViewBoundsProvider = CreateEditorScreenHelper();
#else
			_gameViewBoundsProvider = new UnityPlayerGameViewBoundsProvider();
#endif
            _gazeFocus = new GazeFocus();

            _gazePointDataProvider = new GazePointDataProvider(this);
            _headPoseDataProvider = new HeadPoseDataProvider();
        }

        private void Update()
        {
            if (_gameViewBoundsProvider.Hwnd != IntPtr.Zero)
            {
                Interop.SetWindow(_gameViewBoundsProvider.Hwnd);
                if (!_isConnected)
                {
                    Interop.Start(false);

                    _gazePointDataProvider.Disconnect();
                    _headPoseDataProvider.Disconnect();
                    _isConnected = true;
                }
            }

            SyncData();

            var gameViewBounds = _gameViewBoundsProvider
                .GetGameViewClientAreaNormalizedBounds();
            _gameViewInfo = new GameViewInfo(gameViewBounds);

            _gazeFocus.UpdateGazeFocus();

            StartCoroutine(DoEndOfFrameCleanup());
        }

        private void OnDestroy()
        {
            Shutdown();
        }

        private void OnApplicationQuit()
        {
            Shutdown();
        }

        //--------------------------------------------------------------------
        // Private Functions
        //--------------------------------------------------------------------

        private void SyncData()
        {
            if (_updatedInFrame) return;

            _updatedInFrame = true;

            var result = Interop.Update();
            if (result)
            {
                _gazePointDataProvider.Update();
                _headPoseDataProvider.Update();
            }
        }

        private IEnumerator DoEndOfFrameCleanup()
        {
            yield return new WaitForEndOfFrame();

            _updatedInFrame = false;
            _gazePointDataProvider.EndFrame();
            _headPoseDataProvider.EndFrame();
        }

        private void Disconnect()
        {
            if (_isConnected)
            {
                Interop.Stop();
                _isConnected = false;
            }
        }

#if UNITY_EDITOR
        private static GameViewBoundsProvider CreateEditorScreenHelper()
        {
#if UNITY_4_5 || UNITY_4_3 || UNITY_4_2 || UNITY_4_1
			return new LegacyEditorGameViewBoundsProvider();
#else
            return new EditorGameViewBoundsProvider();
#endif
        }
#endif
    }
}

#else
using Tobii.Gaming.Stubs;

namespace Tobii.Gaming.Internal
{
    internal partial class TobiiHost : TobiiHostStub
    {
        // all implementation in the stub
    }
}
#endif