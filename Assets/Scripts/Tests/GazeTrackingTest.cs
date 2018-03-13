using Assets.Scripts.DataCollection.Physiological.Sensors;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     MAKE SURE THAT THE TOBI GAZE TRACKING PROCESS IS RUNNING! THIS WILL FAIL!
    /// </remarks>
    public class GazeTrackingTest : MonoBehaviour
    {
        public GazeTracking GazeTracking;

        public bool On;

        // Use this for initialization
        private void Start()
        {
            if (!On) return;
            GazeTracking = new GazeTracking();
            if (GazeTracking.GetSensorValue() !=
                GazeTracking.GetSensorFailureValue()) return;
        }
    }
}