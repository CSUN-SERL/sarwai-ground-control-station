using System;
using System.Globalization;
using FeedScreen.Experiment;
using MediaDownload;
using Tobii.Plugins;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mission.Queries.QueryTypes
{
    public class ConfidenceQuery : Query
    {
        [SerializeField]
        public float Confidence { get; set; }

        public override void Display()
        {
            DisplayEventManager.OnDisplayConfidence(
                Confidence.ToString(CultureInfo.CurrentCulture));
        }

        public override void Arrive()
        {

        }

        public override void OnMediaDownloaded(object sender, DownloadMediaEventArgs e) {
            SocketEventManager.OnQueryRecieved(this);
        }

        public override string GetDisplayName()
        {
            return "Confidence Query";
        }

        public override Query Deserialize(JSONNode json)
        {
            throw new NotImplementedException();
        }
    }
}