using System.Collections;
using Mission;
using UnityEngine;

namespace Tests
{
    public class NotificationTester : MonoBehaviour
    {
        public bool On;

        // Use this for initialization
        private void Start()
        {
            if (!On) return;
            StartCoroutine(SendNotification());
        }

        private void Update()
        {
            //if (Input.GetKeyDown("n"))
            //{
            //	StartCoroutine(SendNotification());
            //}


            //StartCoroutine(SendNotification());
        }

        private IEnumerator SendNotification()
        {
            var track = 0;
            var para = "Paramedics";
            var fire = "Firefighters";
            var not1 = "ETA 13 min";
            var not2 = "ETA 10 min";
            var not3 = "ETA 7 min";
            var not4 = "ETA 5 min";
            var not5 = "ETA 4 min";

            yield return new WaitForSeconds(35);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(para, not1));
            //yield return new WaitForSeconds(1F);

            yield return new WaitForSeconds(4);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(fire, not1));
            //yield return new WaitForSeconds(1F);

            yield return new WaitForSeconds(14);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(para, not2));

            yield return new WaitForSeconds(42);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(fire, not3));

            yield return new WaitForSeconds(10);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(para, not4));

            yield return new WaitForSeconds(60);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(fire, not4));

            yield return new WaitForSeconds(12);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(fire, not4));

            yield return new WaitForSeconds(4);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(para, not5));

            yield return new WaitForSeconds(13);
            Debug.Log("Sending Message...");
            MissionEventManager.OnNotificationReceived(
                new Notification(fire, not4));
        }
    }
}