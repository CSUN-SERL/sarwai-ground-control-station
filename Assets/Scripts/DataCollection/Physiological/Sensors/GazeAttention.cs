using System.Collections;
using Assets.Tobii.Framework;
using Tobii.Framework;
using UnityEngine;

namespace Assets.Scripts.DataCollection.Physiological.Sensors
{
    /// <summary>
    ///     Gathers the name of a gameObject attached with <see cref="UIGazeAware"/>, based on gaze position relative to screen.
    /// </summary>
    public class GazeAttention : PhysiologicalMonoBehaviour<string>
    {
        public override string GetSensorFailureValue()
        {
            return "none";
        }

        public override IEnumerator Log()
        {
            while(true)
            {
                yield return new WaitForSeconds(0F);
                var tempObject = TobiiAPI.GetFocusedObject();
                if (tempObject != null)
                    _sensorValue = TobiiAPI.GetFocusedObject().name.ToString();
                else _sensorValue = GetSensorFailureValue();
                //Debug.Log(_sensorValue);
            }
            
        }
    }
}