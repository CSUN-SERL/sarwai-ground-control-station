using System.Collections;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;
using UnityEngine.Networking;

namespace Tests
{
    public class ServerImageDownload : MonoBehaviour
    {
        public bool On;

        private void Start()
        {
            if (!On) return;
            StartCoroutine(TestServerConnection());
            StartCoroutine(RequestImage());
        }

        public IEnumerator TestServerConnection()
        {
            Debug.Log("Testing Server Connection...");

            var www = UnityWebRequest.Get(
                "http://ec2-52-24-126-225.us-west-2.compute.amazonaws.com:81/");
            yield return www.SendWebRequest();

            CheckNetworkError(www);
        }

        public IEnumerator RequestImage()
        {
            Debug.Log("Testing Image Download...");

            var www = UnityWebRequestTexture.GetTexture(
                "http://ec2-52-24-126-225.us-west-2.compute.amazonaws.com:81/download?filename=test.png");
            yield return www.SendWebRequest();

            CheckNetworkError(www);

            var myTexture =
                ((DownloadHandlerTexture) www.downloadHandler).texture;

            Mission.DisplayEventManager.OnDisplayImage(myTexture);
        }

        public void CheckNetworkError(UnityWebRequest www)
        {
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
                Debug.Log("Form upload complete!");
        }
    }
}