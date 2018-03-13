using System.Collections;
using Assets.Tobii.Framework;
using Tobii.Framework;
using UnityEngine;

namespace Assets.Scripts.DataCollection.Physiological.Sensors
{
    public class GazeTracking : PhysiologicalMonoBehaviour<Vector2>
    {
        public override IEnumerator Log()
        {
            while (true)
            {
                yield return new WaitForSeconds(0F);
                var gazePoint = TobiiAPI.GetGazePoint();
                _sensorValue = gazePoint.IsValid
                    ? gazePoint.Screen
                    : GetSensorFailureValue();
            }
        }

        public override Vector2 GetSensorFailureValue()
        {
            return new Vector2(-1F, -1F);
        }
    }
}