using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace FeedScreen.Experiment.Missions.Broadcasts
{
    public class QueryMaker : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey("1"))
                ManualDetection(0);
            else if (Input.GetKey("2"))
                ManualDetection(1);
            else if (Input.GetKey("3"))
                ManualDetection(2);
            else if (Input.GetKey("4"))
                ManualDetection(3);
        }

        public void ManualDetection(int n_feed)
        {
            if (Input.GetKeyDown("q"))
                Debug.Log(string.Format("{0} Detection", n_feed));
            else if (Input.GetKeyDown("w"))
                Debug.Log(string.Format("{0} Tagging", n_feed));
            else if (Input.GetKeyDown("e"))
                Debug.Log(string.Format("{0} Double", n_feed));
            else if (Input.GetKeyDown("r"))
                Debug.Log(string.Format("{0} Audio", n_feed));
        }

        private IEnumerator SendDetection()
        {
            //TODO Create protocol to send a detection from the GCS.
            yield return UnityWebRequest.Post("www.google.com", "Hello World");
        }
    }
}