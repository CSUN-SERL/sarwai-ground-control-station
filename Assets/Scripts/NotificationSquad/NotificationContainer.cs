using System.Collections.Generic;
using Mission;
using UnityEngine;

namespace NotificationSquad
{
    internal class NotificationContainer : MonoBehaviour
    {
        private Queue<Notification> _pendingNotifications;
        public GameObject NotificationPrefab;

        private void OnEnable()
        {
            MissionEventManager.NotificationReceived += OnNotificationReceived;
            _pendingNotifications = new Queue<Notification>();
        }

        private void OnDisable()
        {
            MissionEventManager.NotificationReceived -= OnNotificationReceived;
            _pendingNotifications = null;
        }

        private void Update()
        {
            while (_pendingNotifications.Count > 0)
            {
                var notification = _pendingNotifications.Dequeue();
                var notificationprefab = Instantiate(NotificationPrefab);
                notificationprefab.GetComponent<NotificationButton>()
                    .Notification = notification;
                notificationprefab.name = "Notification";
                notificationprefab.transform.SetParent(gameObject.transform,
                    false);
            }
        }

        public void AddNotification(Notification n)
        {
            _pendingNotifications.Enqueue(n);
        }

        public void OnNotificationReceived(object source,
            NotificationEventArgs e)
        {
            AddNotification(e.Notification);
        }
    }
}