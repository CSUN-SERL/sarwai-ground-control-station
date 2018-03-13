using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
//using UnityEngine.UI;

namespace IOStream
{
    public class ProcessTransmitterWrap : MonoBehaviour
    {
        // FOR TESTING
        //public Text TestText;

        private Process _process;

        // Use this for initialization
        private void Start()
        {
            Debug.Log("Setting up server...");

            ProcessTransmitter.SetupServer();
            // HEY EVERYONE.. DO NOT REMOVE ME
#if !UNITY_EDITOR
		_process = Process.Start("GCS.exe", "3");
#endif
        }

        // Update is called once per frame
        private void Update()
        {
            ProcessTransmitter.SendProcessTimeStep(Time.timeScale);

            //TestText.text = string.Format(
            //	"{0}, {1}, {2}, {3}",
            //	ProcessTransmitter.GetFeedTime(0),
            //	ProcessTransmitter.GetFeedTime(1),
            //	ProcessTransmitter.GetFeedTime(2),
            //	ProcessTransmitter.GetFeedTime(3)
            //);
        }

        private void OnDestroy()
        {
            ProcessTransmitter.ShutdownServer();

#if !UNITY_EDITOR
		_process.Kill();
#endif
        }
    }
}