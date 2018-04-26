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
            double confidence = (Math.Round(double.Parse(Confidence.ToString()) * 100 * 100)) / 100;
            DisplayEventManager.OnDisplayQuestion(string.Format("I am {0}% confident this is a correct detection of a human in the bounding box. Can you please verify my detection?", confidence));
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