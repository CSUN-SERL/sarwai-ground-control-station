using FeedScreen.Experiment.Missions.Broadcasts.Events;
using Mission.Queries.QueryTypes.Audio;
using Mission.Queries.QueryTypes.Visual;
using UnityEngine;

namespace Mission.Queries
{
    public class DetectionQueryFactory : MonoBehaviour
    {
        private void OnEnable()
        {
            //EventManager.QueryRecieved += ParseQuery;
        }

        private void OnDisable()
        {
            //EventManager.QueryRecieved -= ParseQuery;
        }

        private void ParseQuery(object sender, StringEventArgs e)
        {
            Debug.Log("Making new Query: " + e);
        }

        public static Query GetQuery(string type, string data)
        {
            Query query = null;

            switch (type)
            {
                case "Detection":
                    query = JsonUtility.FromJson<VisualDetectionQuery>(data);
                    break;
                case "Tagging":
                    query = JsonUtility.FromJson<TaggingQuery>(data);
                    break;
                case "DoubleDetection":
                    query = JsonUtility.FromJson<DoubleDetectionQuery>(data);
                    break;
                case "Audio":
                    query = JsonUtility.FromJson<AudioDetectionQuery>(data);
                    break;
            }

            return query;
        }
    }
}