using System.Collections;
using Assets.Tobii.Framework;
using Tobii.Framework;
using UnityEngine;

namespace Assets.Scripts.DataCollection.Physiological.Sensors
{
    public static class AttentionId
    {
        public const float Robot1 = 0;
        public const float Robot2 = 1;
        public const float Robot3 = 2;
        public const float Robot4 = 3;
        public const float QueryList = 4;
        public const float QueryQuestion = 5;
        public const float NotificationList = 6;

        /// <summary>
        ///     Uses hardcoded numbers to find the location of the vectory coresponding to an attention_id.
        /// </summary>
        /// <param name="_screenValue"></param>
        /// <returns></returns>
        public static float getAttentionId(Vector2 _screenValue)
        {
            var half_screen = 980;
            var right_side = 2100;
            var attention_id = -1F;
            if (_screenValue.x > half_screen)
            {
                if (_screenValue.y > 550)
                {
                    if (_screenValue.x < half_screen + 470)
                        attention_id = Robot1;
                    else
                        attention_id = Robot2;
                }
                else
                {
                    if (_screenValue.x < half_screen + 470)
                        attention_id = Robot3;
                    else
                        attention_id = Robot4;
                }
            }
            else
            {
                if (_screenValue.x > 760)
                    attention_id = QueryList;
                else if (_screenValue.x < 280)
                    attention_id = NotificationList;
                else
                    attention_id = QueryQuestion;
            }
            return attention_id;
        }
    }
    public class GazeTracking : PhysiologicalMonoBehaviour<Vector3>
    {
        

        public override IEnumerator Log()
        {
            while (true)
            {
                yield return new WaitForSeconds(0F);
                var gazePoint = TobiiAPI.GetGazePoint();
                
                if (gazePoint.IsValid)
                {
                    Vector2 _screenValue = gazePoint.Screen;
                    
                    _sensorValue = new Vector3(_screenValue.x, _screenValue.y, AttentionId.getAttentionId(_screenValue));

                }
                else
                {
                    _sensorValue = GetSensorFailureValue();
                }
                    
                
               // Debug.Log("sensor value is "+_sensorValue +" resolution: " + Screen.currentResolution);
            }
        }

        public override Vector3 GetSensorFailureValue()
        {
            return new Vector3(-1F, -1F,-1F);
        }
    }
}