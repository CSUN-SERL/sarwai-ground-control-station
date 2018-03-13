using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;

namespace TransparencyIrisToOperator
{
    public class EventManager
    {

        public static event EventHandler<EventArgs> Load;
        //public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;

        public static void OnLoad(int i)
        {
            var handler = Load;
            if (handler != null) handler(null, new IntEventArgs { intField = i });
        }
        public static event EventHandler<EventArgs> End;
        //public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;

        public static void OnEnd()
        {
            var handler = End;
            if (handler != null) handler(null, new EventArgs());
        }
    }
}