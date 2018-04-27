using System.Collections;
using System.Collections.Generic;
using Networking;
using Participant;
using Tobii.Plugins;
using UnityEngine;
using UnityEngine.Networking;

public class PerformanceScoresTest : MonoBehaviour {

	private int _victimsIdentified;
	private int _totalVictims;
	private int _missedDetectionsCaptured;
	private int _totalMissedDetections;
	private int _irisCorrect;
	private int _irisHandled;
	private int _fpSentToGcs;
	private int _totalFp;
	

	public int ParticipantId;
	public int MissionId;
	public int Group;

	// Use this for initialization
	void Start () {
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
				Debug.Log(result["data"]);
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
				Debug.Log(result["data"]);
				_victimsIdentified = result["data"].AsInt;
			}
		}

		// Get Missed Detections Captured.
		using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_MISSED_DETECTIONS_CAPTURED, form)) {
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
			} else {

				var result = JSON.Parse(www.downloadHandler.text);
				Debug.Log(result["data"]);
				_missedDetectionsCaptured = result["data"].AsInt;
			}
		}
		// Get Total Missed Detections.
		using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_TOTAL_MISSED_DETECTIONS, form)) {
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
			} else {

				var result = JSON.Parse(www.downloadHandler.text);
				Debug.Log(result["data"]);
				_totalMissedDetections = result["data"].AsInt;
			}
		}

		if (Group == 1 || Group == 2)
		{
			// Retrieve IRIS accuracy
			using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_IRIS_CORRECT, form))
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError)
				{
					Debug.Log(www.error);
				}
				else
				{

					var result = JSON.Parse(www.downloadHandler.text);
					Debug.Log(result["data"]);
					_irisCorrect = result["data"].AsInt;
				}
			}

			using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_IRIS_HANDLED, form))
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError)
				{
					Debug.Log(www.error);
				}
				else
				{

					var result = JSON.Parse(www.downloadHandler.text);
					Debug.Log(result["data"]);
					_irisHandled = result["data"].AsInt;
				}
			}

			// Retrieve adaptation effectiveness
			using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_FP_SENT_TO_GCS, form))
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError)
				{
					Debug.Log(www.error);
				}
				else
				{

					var result = JSON.Parse(www.downloadHandler.text);
					Debug.Log(result["data"]);
					_fpSentToGcs = result["data"].AsInt;
				}
			}

			using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_TOTAL_FP, form))
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError)
				{
					Debug.Log(www.error);
				}
				else
				{

					var result = JSON.Parse(www.downloadHandler.text);
					Debug.Log(result["data"]);
					_totalFp = result["data"].AsInt;
				}
			}

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
						completed = _missedDetectionsCaptured,
						total = _totalMissedDetections
					},
                    new PerformanceMetric
                    {
                        name = "Iris Accuracy",
                        completed = _irisCorrect,
                        total = _irisHandled
                    },
                    new PerformanceMetric
                    {
                        name = "Adaptation Effectiveness",
                        completed = _fpSentToGcs,
                        total = _totalFp
                    }
				}
			});
		}
		else
		{
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
						completed = _missedDetectionsCaptured,
						total = _totalMissedDetections
					}
				}
			});
		}


		

		Debug.Log("Done");
	}
}
