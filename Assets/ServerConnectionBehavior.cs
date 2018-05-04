using System.Collections;
using System.Net;
using UnityEngine;

namespace Networking
{
    public class ServerConnectionBehavior : MonoBehaviour {
        // Sockets that are needed for connecting to server.
        private GcsSocket _gcsSocket;

        private ServerURL _serverUrl;

        public IPEndPoint EndPoint { get; set; }

        private Coroutine _connectingCoroutine;

        // Number of retries before failure to connect.
        public int _numAttempts = 5;

        // Time to wait unitl trying to connect again.
        public float timeoutTime = 1000;

        public static ServerConnectionBehavior Instance;

        void Awake() {
            // Singleton
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        public void Connect(IPEndPoint endPoint)
        {
            EndPoint = endPoint;

            // If already connecting, disconnect and try again.
            if (_connectingCoroutine != null)
            {
                StopCoroutine(_connectingCoroutine);
            }
            _connectingCoroutine = StartCoroutine(ConnectionEnumerator());
        }

        IEnumerator ConnectionEnumerator() {

            // Create Sockets.
            _gcsSocket = new GcsSocket(EndPoint);
            _serverUrl = new ServerURL(EndPoint);

            if (_gcsSocket == null) {
                EventManager.OnConnectionFailed();
                yield break;
            }

            // Attempt connection.
            for (int i = 0; i < _numAttempts; i++) {
                _gcsSocket.ConnectToSocket();
                if (_gcsSocket.Connected)
                {
                    Debug.Log("Connected");
                    
                    yield break;
                }
                else
                {
                    Debug.Log("Could not connect. Retrying...");
                }

                EventManager.OnConnected(EndPoint);
                yield return new WaitForSecondsRealtime(timeoutTime);
            }

            // Connection was unsuccessful.
            EventManager.OnConnectionFailed();
        }

        public void Disconnect()
        {
            _gcsSocket.Disconnect();
        }

        void OnApplicationQuit()
        {
            _gcsSocket.Disconnect();
        }
    }
}
