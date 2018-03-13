using System.Collections;
using System.Collections.Generic;
using Networking;
using Newtonsoft.Json.Linq;
using Participant;
using UnityEngine;
using UnityEngine.Networking;

public class TransparencyBriefTest : MonoBehaviour
{

    public bool On;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("space") && On)
	    {
	        StartCoroutine(GetBriefQueries());
	    }

    }

    IEnumerator GetBriefQueries()
    {
        WWWForm form = new WWWForm();
        form.AddField("participant_id", ParticipantBehavior.Participant.Data.Id);
        form.AddField("mission_number", ParticipantBehavior.Participant.CurrentMission-1);
        form.AddField("num_queries", 5);

        using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_TRANSPARENCY_BRIEF, form)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
                var fuck = JObject.Parse(www.downloadHandler.text)["data"];
                foreach (var f in fuck)
                {
                    Debug.Log(f);
                }
            }
        }
    }
}
