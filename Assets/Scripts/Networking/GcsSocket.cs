using System;
using System.Collections;
using Mission;
using Quobject.SocketIoClientDotNet.Client;
using UnityEngine;
using EventManager = Mission.Lifecycle.EventManager;

namespace Networking
{
    public class GcsSocket : MonoBehaviour
    {
        public static bool Connected;

        public static GcsSocket Instance;

        private Socket _socket;

        public static bool Alive
        {
            get { return Instance._socket != null; }
        }

        private void OnEnable()
        {
            SocketEventManager.DisconnectSocket += OnDisconnectSocket;
        }

        private void OnDisable()
        {
            SocketEventManager.DisconnectSocket -= OnDisconnectSocket;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                ConnectToSocket();
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        IEnumerator EnsureConnection()
        {
            while (true)
            {
                if (!Alive || !Connected)
                {
                    ConnectToSocket();
                }
                yield return new WaitForSecondsRealtime(1);
            }
        }

        private void OnApplicationQuit()
        {
            if (_socket == null) return;
            _socket.Emit("testee", "GCS Disconnecting.");
            _socket.Disconnect();
        }

        public void ConnectToSocket()
        {

            // If already connected, do nothing.
            if (_socket != null) return;

            // The socket is null.
            Connected = false;

            _socket = IO.Socket(ServerURL.SOCKET_IO);
            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                Debug.Log("Connected Event Received.");
                _socket.Emit("testee", "GCS Connected.");
                Connected = true;
                SocketEventManager.OnSocketConnected();
            });

            _socket.On(Socket.EVENT_DISCONNECT, () =>
            {
                Debug.Log("Disconnect Event Received.");
                DoClose();
            });

            // Add listener for test data.
            _socket.On("tester",
                data => { Debug.Log("Testing Data Received:" + data); });

            _socket.On(ServerURL.QUERY_RECEIVED, data =>
            {
                Debug.Log(data);
                SocketEventManager.OnDataRecieved(
                    new StringEventArgs { StringArgs = data.ToString() });
            });

            _socket.On(MissionLifeCycleController.MISSION_INITIALIZED,
                data =>
                {
                    Debug.Log("Mission Initialized Event Received.");
                    EventManager.OnInitialized();
                });

                _socket.On(ServerURL.AUTONOMOUS_QUERY, data =>
                {
                    Debug.Log("Autonomous Query Socket");
                    SocketEventManager.OnAutonomousQuery(data.ToString());
                });
            _socket.On(MissionLifeCycleController.MISSION_STARTED, data =>
            {
                EventManager.OnStarted();
                Debug.Log("Mission Started Event Received.");
            });

                _socket.On(ServerURL.GENERATED_QUERY, data =>
                {
                    Debug.Log("Generated Query Socket");
                    SocketEventManager.OnQueryGenerated(data.ToString());
                });

            }
            _socket.On(MissionLifeCycleController.MISSION_STOPPED, data =>
            {
                EventManager.OnStopped();
                Debug.Log("Mission Stopped Event Received.");
            });

            _socket.On(ServerURL.NOTIFICATION_RECEIVED, data =>
            {
                //EventManager.OnNotificationRecieved(new NotificationEventArgs { Notification = new Notification(data.ToString()) });
                Debug.Log("Notification Event Reveicved.");
            });

        }

        public static void Emit(string socketEvent, string message)
        {
            Debug.Log(string.Format("Emitting {0} on {1}", message, socketEvent));
            Instance._socket.Emit(socketEvent, message);
        }

        public static void Emit(string socketEvent, int message) {
            Debug.Log(string.Format("Emitting {0} on {1}", message, socketEvent));
            Instance._socket.Emit(socketEvent, message);
        }

        private void OnDisconnectSocket(object sender, EventArgs e)
        {
            if (_socket == null) return;
            DoClose();
        }

        void DoClose()
        {
            _socket.Disconnect();
            SocketEventManager.OnSocketDisconnected();
            _socket = null;
            Connected = false;
        }
    }
}