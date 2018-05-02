using System;
using System.Net;
using UnityEngine;

namespace Networking
{
    public class EventManager : MonoBehaviour
    {

        public static event EventHandler<ConnectEventArgs> Connect;

        public static void OnConnect(IPEndPoint endPoint) {
            Debug.Log("Connect Event");
            var handler = Connect;
            if (handler != null) handler(null, new ConnectEventArgs
            {
                EndPoint = endPoint
            });
        }

        public static event EventHandler<ConnectEventArgs> Connected;

        public static void OnConnected(IPEndPoint endPoint)
        {
            Debug.Log("Connected Event");
            var handler = Connected;
            if (handler != null) handler(null, new ConnectEventArgs
            {
                EndPoint = endPoint
            });
        }

        public static event EventHandler<EventArgs> Disconnected;

        public static void OnDisconnected() {
            Debug.Log("Disconnected Event");
            var handler = Disconnected;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> ConnectionFailed;

        public static void OnConnectionFailed()
        {
            Debug.Log("Connect Failed Event");
            var handler = ConnectionFailed;
            if (handler != null) handler(null, EventArgs.Empty);
        }
    }

    public class ConnectEventArgs : EventArgs
    {
        public IPEndPoint EndPoint { get; set; }
    }
}

