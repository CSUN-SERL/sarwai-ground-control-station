using System.Collections;
using System.Data;
using System.Linq;
using DataCollection.Physiological;
using Networking;
using Tobii.Plugins;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.DataCollection.Physiological
{
    public class Uploader : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.UploadData += Upload;
        }

        private void OnDisable()
        {
            EventManager.UploadData -= Upload;
        }

        private void Upload(object sender, PhysiologicalDataEventArgs e)
        {
            Debug.Log("Uploading Data");
            StartCoroutine(Upload(e.DataTable));
        }


        private IEnumerator Upload(DataTable dataTable)
        {
            var form = new WWWForm();

            form.AddField("database", "dbexperiment");

            var values = from DataRow dataRow in dataTable.Rows
                select string.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                    dataRow["participant_id"], dataRow["mission_number"],
                    dataRow["mission_time"], dataRow["heart_rate"],
                    dataRow["galvanic_skin_response"], dataRow["emotion"],
                    dataRow["gaze_x"], dataRow["gaze_y"]);


            var sql = string.Format(
                "INSERT INTO dbexperiment.physiological_data (participant_id, mission_number, mission_time, heart_rate, galvanic_skin_response, emotion, gaze_x, gaze_y) VALUES{0}",
                string.Join(",", values.ToList().ToArray()));

            Debug.Log(sql);

            form.AddField("sql", sql);

            using (var www = UnityWebRequest.Post(ServerURL.INSERT, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    var result = JSON.Parse(www.downloadHandler.text);
                    if (result["failed"].AsBool)
                        Debug.Log("Could not upload physiological data.");
                    else
                        Debug.Log("Physiological Data Uploaded");
                }
            }

            EventManager.OnDataUploaded();
        }
    }
}