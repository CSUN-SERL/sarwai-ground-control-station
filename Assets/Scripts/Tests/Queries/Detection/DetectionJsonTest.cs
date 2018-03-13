using MediaDownload;
using Mission.Queries.QueryTypes.Visual;
using UnityEngine;

namespace Tests.Queries.Detection
{
    public class DetectionJsonTest : MonoBehaviour
    {
        public bool On;
        public string QueryType;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("space") && On)
            {
                var query = new VisualDetectionQuery
                {
                    ArrivalTime = 0,
                    Confidence = 0f,
                    QueryId = 0,
                    RobotId = 0,
                    UserId = 0,
                    ImageFileName = "image-robot-4-1323.png"
                };
                Debug.Log("Sending Test Query");
                query.Arrive();
            }
        }
    }
}