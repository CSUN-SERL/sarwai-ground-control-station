using System.Collections;
using System.Net;
using Mission;
using Mission.Lifecycle;
using Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

namespace Networking
{
    /// <summary>
    /// The socket for socket.io.
    /// </summary>
    public class GcsSocket
    {
        // Socket Event Names
        public const string QueryReceived = "gcs-query-received";
        public const string NotificationReceived = "gcs-notification-received";
        public const string SendAnswerQuery = "gcs-query-answers";
        public const string AutonomousQuery = "gcs-automated-query";
        public const string GeneratedQuery = "gcs-generated-query";
        public const string ToggleManualControl = "cm-toggle-manual-control";


        //// Instance for the singleton.
        public static GcsSocket Instance;

        private readonly IPEndPoint _endpoint;

        private Socket _socket;

        /// <summary>
        ///     If the socket is connected.
        /// </summary>
        public bool Connected { get; set; }

        public GcsSocket(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
            Debug.Log(endpoint);
            Instance = this;
        }

        //public static bool Alive
        //{
        //    get { return Instance._socket != null; }
        //}

        ///// <summary>
        ///// Makes sure that there is only one instance of this object at a time.
        ///// </summary>
        //private void Awake() {
        //    if (Instance == null) {
        //        Instance = this;
        //    } else if (Instance != this) {
        //        Destroy(
        //    }
        //}

        ///// <summary>
        ///// Makes sure that the scene flow controller does not get deleted when moving from scene to scene.
        ///// </summary>
        //private void Start()
        //{
        //    DontDestroyOnLoad(gameObject);
        //}

        /// <summary>
        ///     This function checks to see if the socket is connected and if it is not, then try to reconnect.
        /// </summary>
        /// <returns></returns>
        //private IEnumerator EnsureConnection()
        //{
        //    while (true)
        //    {
        //        if (!Alive || !Connected)
        //            ConnectToSocket();
        //        yield return new WaitForSecondsRealtime(1);
        //    }
        //}


        /// <summary>
        ///     Disconnect from the socket when the application quits.
        /// </summary>
        private void OnApplicationQuit()
        {
            if (_socket == null) return;
            _socket.Emit("testee", "GCS Disconnecting.");
            _socket.Disconnect();
        }


        /// <summary>
        ///     Contains all routines to run when connecting to socket.
        /// </summary>
        public void ConnectToSocket()
        {
            // If already connected, disconnect from socket and reconnect.
            if (_socket != null || Connected)
                Disconnect();

            Debug.Log("Creating Socket");

            //Create the socket instance.
            _socket = IO.Socket(string.Format("http://{0}:{1}/socket.io", _endpoint.Address, _endpoint.Port));

            // The section below is all of the socket events for the GCS to listen to.

            // When the socket connects.
            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                Debug.Log("Connected Event Received.");
                _socket.Emit("testee", "GCS Connected.");
                Connected = true;

                // Emit to the world that the socket has been connected.
                EventManager.OnConnected(Instance._endpoint);
            });


            // When the socket disconnects.
            _socket.On(Socket.EVENT_DISCONNECT, () =>
            {
                Debug.Log("Disconnect Event Received.");
                Disconnect();
            });

            // Add listener for test data.
            _socket.On("tester",
                data => { Debug.Log("Testing Data Received:" + data); });

            // When a query is received from the server.
            _socket.On(QueryReceived, data =>
            {
                Debug.Log(data);
                SocketEventManager.OnDataRecieved(
                    new StringEventArgs {StringArgs = data.ToString()});
            });


            // Then the mission has been initialized.
            _socket.On(MissionLifeCycleController.MISSION_INITIALIZED,
                data =>
                {
                    Debug.Log("Mission Initialized Event Received.");
                    Mission.Lifecycle.EventManager.OnInitialized();
                });


            // When a query has been handled autonomously.
            _socket.On(AutonomousQuery, data =>
            {
                Debug.Log("Autonomous Query Socket");
                SocketEventManager.OnAutonomousQuery(data.ToString());
            });


            // When a query has been handled non autonomously.
            _socket.On(GeneratedQuery, data =>
            {
                Debug.Log("Generated Query Socket");
                SocketEventManager.OnQueryGenerated(data.ToString());
            });


            // When the mission has been started by the server.
            _socket.On(MissionLifeCycleController.MISSION_STARTED, data =>
            {
                Mission.Lifecycle.EventManager.OnStarted();
                Debug.Log("Mission Started Event Received.");
            });


            // When the mission has been stopped by the server.
            _socket.On(MissionLifeCycleController.MISSION_STOPPED, data =>
            {
                Mission.Lifecycle.EventManager.OnStopped();
                Debug.Log("Mission Stopped Event Received.");
            });


            // When a notification is received.
            _socket.On(NotificationReceived, data =>
            {
                //EventManager.OnNotificationRecieved(new NotificationEventArgs { Notification = new Notification(data.ToString()) });
                Debug.Log("Notification Event Reveicved.");
            });
        }

        /// <summary>
        ///     Emit a string on a socket event.
        /// </summary>
        /// <param name="socketEvent">The event name to be emitted to.</param>
        /// <param name="message">The message to be sent on the socket event.</param>
        public static void Emit(string socketEvent, string message)
        {
            Debug.Log(string.Format("Emitting {0} on {1}", message, socketEvent));
            Instance._socket.Emit(socketEvent, message);
        }


        /// <summary>
        ///     Emit an int on a socket event.
        /// </summary>
        /// <param name="socketEvent">The event name to be emitted to.</param>
        /// <param name="message">The message to be sent on the socket event.</param>
        public static void Emit(string socketEvent, int message)
        {
            Debug.Log(string.Format("Emitting {0} on {1}", message, socketEvent));
            Instance._socket.Emit(socketEvent, message);
        }


        public void Disconnect()
        {
            if (_socket == null) return;
            _socket.Disconnect();
            SocketEventManager.OnSocketDisconnected();
            _socket = null;
            Connected = false;
        }
    }
}