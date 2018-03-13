using FeedScreen.Experiment.Missions.Broadcasts.Events;

namespace Mission.Queries.QueryTypes.Visual
{
    public class DoubleDetectionQuery : VisualQuery
    {
        public const string QueryType = "double-detection";

        public override void Display()
        {
            base.Display();
            DisplayEventManager.OnDisplayQuestion(
                "Is this a double detection?");
        }

        public override string GetDisplayName()
        {
            return "Double Detection";
        }
    }
}