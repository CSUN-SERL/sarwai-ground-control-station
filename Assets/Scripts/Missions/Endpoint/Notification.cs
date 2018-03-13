using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace Mission
{
    [Serializable]
    public class Notification : MonoBehaviour
    {
        public Notification(string sender, string message)
        {
            //NotificationId = id;
            Sender = sender;
            Message = message;
        }

        [SerializeField]
        public string Sender { get; private set; }

        [SerializeField]
        public string Message { get; private set; }

        public string GetTypeString()
        {
            return Sender;
        }

        public override string ToString()
        {
            //return string.Format("NID:{0}, Message{1}", NotificationId, Message);
            return string.Format("Notification : Sender {0} ... Message '{1}'",
                Sender, Message);
        }

        public string ShowNotification()
        {
            return string.Format("{0}", Message);
        }

        public void Display()
        {
            DisplayEventManager.OnDisplayNotification(this);
        }

        public void Destroy()
        {
            GameObject.Destroy(this);
        }
    }
}