using Mission.Queries.QueryTypes.Audio;
using UnityEngine;

namespace Tests.Queries.Audio
{
    public class DisplayTest : MonoBehaviour
    {
        public AudioClip Clip;

        public bool On;
        public Texture WaveTexture;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("space") && On)
            {
                Debug.Log("Test");
                Mission.SocketEventManager.OnQueryRecieved(new AudioDetectionQuery
                {
                    ArrivalTime = 0.0F,
                    Audio = Clip,
                    Confidence = 80F,
                    QueryId = 0,
                    Response = 0,
                    RobotId = 0,
                    UserId = 0,
                    Texture = WaveTexture
                });
            }
        }
    }
}