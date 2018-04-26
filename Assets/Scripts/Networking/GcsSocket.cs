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
        /// <summary>
        /// If the socket is connected.
        /// </summary>
        public static bool Connected;


        // Instance for the singleton.
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

        /// <summary>
        /// Makes sure that there is only one instance of this object at a time.
        /// </summary>
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

        /// <summary>
        /// Makes sure that the scene flow controller does not get deleted when moving from scene to scene.
        /// </summary>
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// This function checks to see if the socket is connected and if it is not, then try to reconnect.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Disconnect from the socket when the application quits.
        /// </summary>
        private void OnApplicationQuit()
        {
            if (_socket == null) return;
            _socket.Emit("testee", "GCS Disconnecting.");
            _socket.Disconnect();
        }


        /// <summary>
        /// Contains all routines to run when connecting to socket.
        /// </summary>
        public void ConnectToSocket()
        {

            // If already connected, do nothing.
            if (_socket != null) return;

            // By this point, the socket is null.
            Connected = false;

            //Create the socket instance.
            _socket = IO.Socket(ServerURL.SOCKET_IO);


            ///
            /// The section below is all of the socket events for the GCS to listen to.
            /// 

            // When the socket connects.
            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                Debug.Log("Connected Event Received.");
                _socket.Emit("testee", "GCS Connected.");
                Connected = true;

                // Emit to the world that the socket has been connected.
                SocketEventManager.OnSocketConnected();
            });


            // When the socket disconnects.
            _socket.On(Socket.EVENT_DISCONNECT, () =>
            {
                Debug.Log("Disconnect Event Received.");
                DoClose();
            });

            // Add listener for test data.
            _socket.On("tester",
                data => { Debug.Log("Testing Data Received:" + data); });

            // When a query is received from the server.
            _socket.On(ServerURL.QUERY_RECEIVED, data =>
            {
                Debug.Log(data);
                SocketEventManager.OnDataRecieved(
                    new StringEventArgs { StringArgs = data.ToString() });
            });

            
            // Then the mission has been initialized.
            _socket.On(MissionLifeCycleController.MISSION_INITIALIZED,
                data =>
                {
                    Debug.Log("Mission Initialized Event Received.");
                    EventManager.OnInitialized();
                });


            // When a query has been handled autonomously.
            _socket.On(ServerURL.AUTONOMOUS_QUERY, data =>
            {
                Debug.Log("Autonomous Query Socket");
                SocketEventManager.OnAutonomousQuery(data.ToString());
            });


            // When a query has been handled non autonomously.
            _socket.On(ServerURL.GENERATED_QUERY, data =>
            {
                Debug.Log("Generated Query Socket");
                SocketEventManager.OnQueryGenerated(data.ToString());
            });

            
            // When the mission has been started by the server.
            _socket.On(MissionLifeCycleController.MISSION_STARTED, data =>
            {
                EventManager.OnStarted();
                Debug.Log("Mission Started Event Received.");
            });


            // When the mission has been stopped by the server.
            _socket.On(MissionLifeCycleController.MISSION_STOPPED, data =>
            {
                EventManager.OnStopped();
                Debug.Log("Mission Stopped Event Received.");
            });


            // When a notification is received.
            _socket.On(ServerURL.NOTIFICATION_RECEIVED, data =>
            {
                //EventManager.OnNotificationRecieved(new NotificationEventArgs { Notification = new Notification(data.ToString()) });
                Debug.Log("Notification Event Reveicved.");
            });

        }

        /// <summary>
        /// Emit a string on a socket event.
        /// </summary>
        /// <param name="socketEvent">The event name to be emitted to.</param>
        /// <param name="message">The message to be sent on the socket event.</param>
        public static void Emit(string socketEvent, string message)
        {
            Debug.Log(string.Format("Emitting {0} on {1}", message, socketEvent));
            Instance._socket.Emit(socketEvent, message);
        }


        /// <summary>
        /// Emit an int on a socket event.
        /// </summary>
        /// <param name="socketEvent">The event name to be emitted to.</param>
        /// <param name="message">The message to be sent on the socket event.</param>
        public static void Emit(string socketEvent, int message) {
            Debug.Log(string.Format("Emitting {0} on {1}", message, socketEvent));
            Instance._socket.Emit(socketEvent, message);
        }

        /// <summary>
        /// Routine for when the socket is disconnected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisconnectSocket(object sender, EventArgs e)
        {
            if (_socket == null) return;
            DoClose();
        }

        
        /// <summary>
        /// Routine to close the socket connection.
        /// </summary>
        void DoClose()
        {
            _socket.Disconnect();
            SocketEventManager.OnSocketDisconnected();
            _socket = null;
            Connected = false;
        }
    }
}