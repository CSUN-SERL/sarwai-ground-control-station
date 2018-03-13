using Mission;
using Mission.Queries.QueryTypes.Audio;
using Mission.Queries.QueryTypes.Visual;
using Participant;
using Tobii.Plugins;
using UnityEngine;

namespace Networking
{
    public class EndpointDataReceiver : MonoBehaviour
    {
        public static EndpointDataReceiver Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            SocketEventManager.DataRecieved += OnDataRecieved;
        }

        private void OnDisable()
        {
            SocketEventManager.DataRecieved -= OnDataRecieved;
        }

        private void OnDataRecieved(object sender, StringEventArgs e)
        {
            OnDataRecieved(e.StringArgs);
        }

        public static void OnDataRecieved(string queryJson)
        {
            Debug.Log(queryJson);
            var json = JSON.Parse(queryJson);

            var data = json["data"];

            Query query = null;
            switch (json["type"])
            {
                case VisualDetectionQuery.QueryType:
                    query = new VisualDetectionQuery
                    {
                        ArrivalTime = MissionTimer.CurrentTime,
                        Confidence = data["confidence"].AsFloat,
                        QueryId = data["query_id"].AsInt,
                        RobotId = data["robot_id"].AsInt,
                        UserId = ParticipantBehavior.Participant.CurrentMission,
                        ImageFileName = data["file_path"]
                    };
                    break;

                case AudioDetectionQuery.QueryType:
                    query = new AudioDetectionQuery
                    {
                        ArrivalTime = MissionTimer.CurrentTime,
                        Confidence = data["confidence"].AsFloat,
                        QueryId = data["query_id"].AsInt,
                        RobotId = data["robot_id"].AsInt,
                        UserId = ParticipantBehavior.Participant.CurrentMission,
                        AudioFileName = data["file_path"]
                    };
                    break;
                default:
                    Debug.Log("Query Type Not Recognized.");
                    break;
            }

            if (query != null)
            {
                Debug.Log("Query Arriving");
                query.Arrive();
            }
        }
        
    }
}