using System.Collections.Generic;
using Networking;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Tests
{
    public class JsonPostTest
    {
        public static IEnumerator UploadString()
        {
            var dic =
                new Dictionary<string, int[]>();


            dic.Add("level_of_autonomy", new[] {0, 0, 1, 0});
            dic.Add("question_id", new[] {10, 11, 12, 13});

            var json_dic = JsonConvert.SerializeObject(dic,Formatting.Indented);
            
            Debug.Log(json_dic);
            var form = new WWWForm();
            form.AddField("json", json_dic);
            using (var www = UnityWebRequest.Post(string.Format("{0}:{1}/test/testing", ServerConnectionBehavior.Instance.EndPoint.Address, ServerConnectionBehavior.Instance.EndPoint.Address), form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text + " is result");
                }
            }
        }
    }
}