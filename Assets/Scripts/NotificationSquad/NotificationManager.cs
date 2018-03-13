using System;
using UnityEngine;

namespace Mission
{
    public class NotificationManager : MonoBehaviour
    {
        public static event EventHandler<StringEventArgs> DisplayNotification;

        public static void OnNotificationRecieved(string message)
        {
            var handler = DisplayNotification;
            if (handler != null)
                handler(null, new StringEventArgs {StringArgs = message});
            Debug.Log("Message: " + message);
        }
    }
}