using System;
using Tobii.Plugins;
using UnityEngine;

namespace Mission
{
    [Serializable]
    public abstract class Query
    {
        public const string QueryType = "query";

        [SerializeField]
        public int QueryId { get; set; }

        [SerializeField]
        public int UserId { get; set; }

        [SerializeField]
        public int RobotId { get; set; }

        [SerializeField]
        public float ArrivalTime { get; set; }

        [SerializeField]
        public int Response { get; set; }

        [SerializeField]
        public int LevelOfAutonomy { get; set; }

        [SerializeField]
        public int PreferredLevelOfAutonomy { get; set; }

        [SerializeField]
        public Guid MediaGuid { get; set; }

        public abstract void Display();

        public abstract void Arrive();

        public abstract void OnMediaDownloaded(object sender, MediaDownload.DownloadMediaEventArgs mediaEventArgs);

        public abstract string GetDisplayName();

        public abstract Query Deserialize(JSONNode json);

        public override string ToString()
        {
            return string.Format(
                "QID:{0}, Robot ID: {1}, Arrival Time: {2}, Response: {3}, Type:{4}",
                QueryId, RobotId, ArrivalTime, Response);
        }
    }
}