using Assets.Scripts.DataCollection.Physiological.Sensors;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    public class HeartRateTest : MonoBehaviour
    {
        public HeartRate HeartRate;

        public bool On;

        // Update is called once per frame
        private void Update()
        {
            if (!On) return;
            Debug.Log(HeartRate.GetSensorValue());
        }
    }
}