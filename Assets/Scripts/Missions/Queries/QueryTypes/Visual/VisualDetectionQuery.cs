using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;

namespace Mission.Queries.QueryTypes.Visual
{
    [Serializable]
    public class VisualDetectionQuery : VisualQuery
    {
        public const string QueryType = "visual-detection";

        public override void Display()
        {
            base.Display();
            DisplayEventManager.OnBoolQuestion(this);
            DisplayEventManager.OnDisplayQuestion("Do you see a victim?");
        }

        public static void Download()
        {
        }

        public override string GetDisplayName()
        {
            return "Visual Detection";
        }
    }
}