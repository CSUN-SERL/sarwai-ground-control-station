using System;
using System.Collections.Generic;

namespace DataCollection.Physiological.Sensors
{
    [Serializable]
    public class NeuLogSensorValue
    {
        public List<float> GetSensorValue;

        public float Value
        {
            get { return GetSensorValue[0]; }
        }

        public float GetValue()
        {
            return GetSensorValue[0];
        }
    }
}