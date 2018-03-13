using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DataCollection.Physiological.Sensors
{
    public class EmotionClassification : PhysiologicalMonoBehaviour<int>
    {
        public override int GetSensorFailureValue()
        {
            return -1;
        }

        public override IEnumerator Log()
        {
            yield return new WaitForSeconds(1F);
        }
    }
}