using System.Collections;
using DataCollection.Physiological.Sensors;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.DataCollection.Physiological.Sensors
{
    public class GalvanicSkinResponse : PhysiologicalMonoBehaviour<float>
    {
        public override IEnumerator Log()
        {
            while (true)
            {
                yield return new WaitForSeconds(1F);

                var www = UnityWebRequest.Get(
                    "http://localhost:22002/NeuLogAPI?GetSensorValue:[GSR],[1]");
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                    Debug.Log(www.error);
                else
                    _sensorValue = JsonUtility
                        .FromJson<NeuLogSensorValue>(www.downloadHandler.text)
                        .GetValue();
            }
        }

        public override float GetSensorFailureValue()
        {
            return -1F;
        }
    }
}