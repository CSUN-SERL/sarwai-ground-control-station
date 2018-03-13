using MediaDownload;
using Mission.Queries.QueryTypes.Audio;
using Mission.Queries.QueryTypes.Visual;
using UnityEngine;

namespace Tests.Queries.Audio
{
    public class AudioJsonTest : MonoBehaviour
    {
        public bool On;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("space") && On)
            {
                var query = new AudioDetectionQuery
                {
                    ArrivalTime = 0,
                    Confidence = 0f,
                    QueryId = 0,
                    RobotId = 0,
                    UserId = 0,
                    AudioFileName = "audio0.ogg"
                };
                query.Arrive();
            }
        }
    }
}