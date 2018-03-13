//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using Tobii.Framework.Internal;
using UnityEngine;
using GazePoint = Tobii.Framework.GazePoint;

namespace Assets.Tobii.Framework.Internal
{
    /// <summary>
    ///     Provider of gaze point data. When the provider has been started it
    ///     will continuously update the Last property with the latest gaze point
    ///     value received from Tobii Engine.
    /// </summary>
    internal class GazePointDataProvider : global::Tobii.Framework.Internal.DataProviderBase<GazePoint>
    {
        private readonly ITobiiHost _tobiiHost;

        /// <summary>
        ///     Creates a new instance.
        ///     Note: don't create instances of this class directly. Use the <see cref="TobiiHost.GetGazePointDataProvider" />
        ///     method instead.
        /// </summary>
        /// <param name="eyeTrackingHost">Eye Tracking Host.</param>
        public GazePointDataProvider(ITobiiHost tobiiHost)
        {
            _tobiiHost = tobiiHost;
            Last = global::Tobii.Framework.GazePoint.Invalid;
        }

        internal override string Id
        {
            get { return "GazePointDataStream"; }
        }

        protected override void OnStreamingStarted()
        {
            Interop.SubscribeToStream(TobiiSubscription
                .TobiiSubscriptionStandardGaze);
        }

        protected override void OnStreamingStopped()
        {
            Interop.UnsubscribeFromStream(TobiiSubscription
                .TobiiSubscriptionStandardGaze);
        }

        internal void Update()
        {
            var gazePoints = Interop.GetNewGazePoints(UnitType.Normalized);
            foreach (var gazePoint in gazePoints) OnGazePoint(gazePoint);

            Cleanup();
        }

        private void OnGazePoint(global::Tobii.Framework.Internal.GazePoint gazePoint)
        {
            var eyetrackerCurrentUs =
                gazePoint
                    .TimeStampMicroSeconds; // TODO awaiting new API from tgi;
            var timeStampUnityUnscaled =
                Time.unscaledTime -
                (eyetrackerCurrentUs - gazePoint.TimeStampMicroSeconds) /
                1000000f;

            var bounds = _tobiiHost.GameViewInfo.NormalizedClientAreaBounds;

            if (float.IsNaN(bounds.x)
                || float.IsNaN(bounds.y)
                || float.IsNaN(bounds.width)
                || float.IsNaN(bounds.height)
                || bounds.width < float.Epsilon
                || bounds.height < float.Epsilon)
                return;

            var x = (gazePoint.X - bounds.x) / bounds.width;
            var y = (gazePoint.Y - bounds.y) / bounds.height;
            Last = new global::Tobii.Framework.GazePoint(new Vector2(x, 1 - y),
                timeStampUnityUnscaled, gazePoint.TimeStampMicroSeconds);
        }
    }
}
#endif