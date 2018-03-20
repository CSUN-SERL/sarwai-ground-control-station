using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace Mission
{
    public class MissionEventManager : MonoBehaviour
    {
        public static event EventHandler<IntEventArgs> ManualDetection;

        public static void OnManualDetection(int robotid)
        {
            if (ManualDetection != null)
                ManualDetection(null, new IntEventArgs());
        }

        public static event EventHandler<NotificationEventArgs>
            NotificationReceived;

        public static void OnNotificationReceived(Notification notification)
        {
            var handler = NotificationReceived;
            Debug.Log("" + notification);
            if (handler != null)
                handler(null,
                    new NotificationEventArgs {Notification = notification});
        }

        public static event EventHandler<QueryEventArgs> QueryAnswered;

        public static void OnQueryAnswered(Query query)
        {
            if (QueryAnswered != null)
                //query.UIDeparture = MissionTimer.CurrentTime;
                //query.TotUiTime = query.UIArrival - query.UIDeparture; 

                query.DepartureTime = MissionTimer.CurrentTime;
                query.UIDeparture = MissionTimer.CurrentTime;
                query.TotUiTime = query.UIArrival - query.UIDeparture;
                query.TotTime = query.ArrivalTime - query.DepartureTime;
                Console.Write("Total UI time for this query : " + query.TotUiTime);
                Console.WriteLine("Total Time for this query : " + query.TotTime);

            QueryAnswered(null, new QueryEventArgs {Query = query});
        }

        public static event EventHandler<AudioEventArgs> PlayAudioClip;

        public void OnPlayAudioClip(AudioEventArgs e)
        {
            var handler = PlayAudioClip;
            if (handler != null) handler(null, e);
        }
    }
}