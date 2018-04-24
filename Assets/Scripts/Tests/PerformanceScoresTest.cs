using System.Collections;
using System.Collections.Generic;
using Networking;
using Participant;
using Tobii.Plugins;
using UnityEngine;
using UnityEngine.Networking;

public class PerformanceScoresTest : MonoBehaviour {
    private int _victimsIdentified = 12;
    private int _totalVictims = 50;

    public int ParticipantId;
    public int MissionId;

    // Use this for initialization
    void Start () {

        //EventManager.OnPerformanceMetricsFetched(new PerformanceScoreEventArgs {
        //    PerformanceMetric = new List<PerformanceMetric>
        //    {
        //        new PerformanceMetric
        //        {
        //            name = "Victims Identified",
        //            completed = _victimsIdentified,
        //            total = _totalVictims
        //        },
        //        new PerformanceMetric
        //        {
        //            name = "Missed Detections Captured",
        //            completed = 12,
        //            total = 37
        //        }
        //    }
        //});
        StartCoroutine(FetchPerformanceScores());
	}



    public IEnumerator FetchPerformanceScores()
    {


        WWWForm form = new WWWForm();
        form.AddField("participant_id", ParticipantId);
        form.AddField("mission_id", MissionId);

        // Get Total Victims.
        using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_TOTAL_VICTIMS, form)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {

                var result = JSON.Parse(www.downloadHandler.text);
                _totalVictims = result["data"].AsInt;
            }
        }

        // Get Victims Identified.
        using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_VICTIMS_IDENTIFIED, form)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {

                var result = JSON.Parse(www.downloadHandler.text);
                _victimsIdentified = result["data"].AsInt;
            }
        }

        // Get Total Missed Detections.

        // Get Missed Detections Captured.

        Debug.Log("Done");


        EventManager.OnPerformanceMetricsFetched(new PerformanceScoreEventArgs {
            PerformanceMetric = new List<PerformanceMetric>
            {
                new PerformanceMetric
                {
                    name = "Victims Identified",
                    completed = _victimsIdentified,
                    total = _totalVictims
                },
                new PerformanceMetric
                {
                    name = "Missed Detections Captured",
                    completed = 12,
                    total = 37
                }
            }
        });

        Debug.Log("Done");
    }
}
