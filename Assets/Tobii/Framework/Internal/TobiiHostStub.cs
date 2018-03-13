//-----------------------------------------------------------------------
// Copyright 2016 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------

using Tobii.Framework;
using Tobii.Framework.Internal;
using UnityEngine;

namespace Assets.Tobii.Framework.Internal
{
    internal class TobiiHostStub : ITobiiHost
    {
        private static TobiiHostStub _instance;

        public IGazeFocus GazeFocus
        {
            get { return new GazeFocusStub(); }
        }

        public DisplayInfo DisplayInfo
        {
            get { return DisplayInfo.Invalid; }
        }

        public global::Tobii.Framework.UserPresence UserPresence
        {
            get { return global::Tobii.Framework.UserPresence.Unknown; }
        }

        public bool IsInitialized
        {
            get { return false; }
        }

        public GameViewInfo GameViewInfo
        {
            get
            {
                return new GameViewInfo(new Rect(float.NaN, float.NaN,
                    float.NaN, float.NaN));
            }
        }

        public IDataProvider<global::Tobii.Framework.GazePoint> GetGazePointDataProvider()
        {
            return new GazePointDataProviderStub();
        }

        public IDataProvider<global::Tobii.Framework.HeadPose> GetHeadPoseDataProvider()
        {
            return new HeadPoseDataProviderStub();
        }

        public void Shutdown()
        {
            /** no implementation **/
        }

        public int GetInstanceID()
        {
            return 0;
        }

        public static ITobiiHost GetInstance()
        {
            if (_instance == null) _instance = new TobiiHostStub();

            return _instance;
        }

        internal GameViewInfo GetGameViewInfo()
        {
            return new GameViewInfo(new Rect(float.NaN, float.NaN, float.NaN,
                float.NaN));
        }

        public static implicit operator bool(TobiiHostStub exists)
        {
            return null != exists;
        }
    }
}