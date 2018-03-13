using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace Mission.Queries.QueryTypes.Visual
{
    [Serializable]
    public class TaggingQuery : VisualQuery
    {
        public const string QueryType = "tagging";

        [SerializeField]
        public string Tag { get; set; }

        public static string Green
        {
            get { return "green"; }
        }

        public static string Yellow
        {
            get { return "yellow"; }
        }

        public static string Red
        {
            get { return "red"; }
        }

        public static string Black
        {
            get { return "black"; }
        }

        public override void Display()
        {
            base.Display();
            DisplayEventManager.OnTagQuestion(this);
        }

        public override string GetDisplayName()
        {
            return "Tag Victim";
        }
    }
}