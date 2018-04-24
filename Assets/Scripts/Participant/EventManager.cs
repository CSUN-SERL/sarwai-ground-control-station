using System;
using UnityEngine;

namespace Participant
{
    public class EventManager : MonoBehaviour
    {
        public static event EventHandler<NewParticipantEventArgs> MakeNewParticipant;

        public static void OnMakeNewParticipant(NewParticipantEventArgs e)
        {
            var handler = MakeNewParticipant;
            if (handler != null) handler(null, e);
        }

        public static event EventHandler<EventArgs>
            NewParticipantMade;

        public static void OnNewParticipantMade()
        {
            var handler = NewParticipantMade;
            if (handler != null) handler(null, new EventArgs());
        }


        public static event EventHandler<NewParticipantEventArgs> FetchPerformanceMetrics;

        public static void OnFetchPerformanceMetrics(Participant e)
        {
            var handler = FetchPerformanceMetrics;
            if (handler != null) handler(null, new NewParticipantEventArgs
            {
                Data = e.Data
            });
        }

        public static event EventHandler<PerformanceScoreEventArgs> PerformanceMetricsFetched;

        public static void OnPerformanceMetricsFetched(PerformanceScoreEventArgs e)
        {
            var handler = PerformanceMetricsFetched;
            if (handler != null) handler(null, e);
        }
    }
}