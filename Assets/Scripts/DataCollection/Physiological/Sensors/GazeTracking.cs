﻿using System.Collections;
using Assets.Tobii.Framework;
using Tobii.Framework;
using UnityEngine;

namespace Assets.Scripts.DataCollection.Physiological.Sensors
{
    /// <summary>
    ///     Gatheres the location of the gaze, specifically, the x and y relative to the screen.
    /// </summary>
    public class GazeTracking : PhysiologicalMonoBehaviour<Vector2>
    {
        public override IEnumerator Log()
        {
            while (true)
            {
                yield return new WaitForSeconds(0F);
                var gazePoint = TobiiAPI.GetGazePoint();
                
                if (gazePoint.IsValid)
                    _sensorValue = gazePoint.Screen;
                else
                    _sensorValue = GetSensorFailureValue();
            }
        }

        public override Vector2 GetSensorFailureValue()
        {
            return new Vector2(-1F, -1F);
        }
    }
}