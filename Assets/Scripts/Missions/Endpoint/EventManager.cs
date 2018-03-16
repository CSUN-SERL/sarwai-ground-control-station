using System;
using UnityEngine;

namespace Mission
{
    public class SocketEventManager : MonoBehaviour
    {
        public static event EventHandler<StringEventArgs> DataRecieved;

        public static void OnDataRecieved(StringEventArgs e)
        {
            var handler = DataRecieved;
            if (handler != null) handler(null, e);
        }

        public static event EventHandler<QueryEventArgs> ReceiveQuery;

        public static void OnReceiveQuery(Query query)
        {
            var handler = ReceiveQuery;
            if (handler != null)
                handler(null, new QueryEventArgs {Query = query});
        }

        public static event EventHandler<QueryEventArgs> QueryRecieved;

        public static void OnQueryRecieved(Query query)
        {
            Debug.Log("Query received event triggered.");
            var handler = QueryRecieved;
            if (handler != null)
                query.ArrivalTime = MissionTimer.CurrentTime;
                query.UIArrivalTime = MissionTimer.CurrentTime;
                Debug.Log(query.ArrivalTime);
                handler(null, new QueryEventArgs {Query = query});
        }

        public static event EventHandler<NotificationEventArgs>
            NotificationRecieved;

        public static void OnNotificationRecieved(NotificationEventArgs e)
        {
            var handler = NotificationRecieved;
            //if (handler != null) handler(null, new NotificationEventArgs { Notification = new Notification(e) });
            if (handler != null) handler(null, e);
        }

        public static event EventHandler<EventArgs> ConnectSocket;

        public static void OnConnectSocket()
        {
            var handler = ConnectSocket;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> SocketConnected;

        public static void OnSocketConnected()
        {
            var handler = SocketConnected;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> DisconnectSocket;

        public static void OnDisconnectSocket()
        {
            var handler = DisconnectSocket;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> SocketDisconnected;

        public static void OnSocketDisconnected()
        {
            var handler = SocketDisconnected;
            if (handler != null) handler(null, EventArgs.Empty);
        }


        public delegate void SocketEvent<T>(T obj, EventArgs args);
        // Handle Query Generated Event.  Used in QueryIndicator to make stuff flash.
        public static event SocketEvent<string> QueryGenerated;

        public static void OnQueryGenerated(string data)
        {
            if (QueryGenerated != null)
                QueryGenerated.Invoke(data, EventArgs.Empty);
            Debug.Log("Triggered: OnQueryGenerated " + data);
        }
    }
}