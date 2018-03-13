using System;
using Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

namespace Tests
{
    public class SocketTest : MonoBehaviour
    {
        private string _testString = "Hello World";
        public bool On;

        public string serverURL;

        protected Socket socket;

        private void OnEnable()
        {
            if (On)
            {
                Mission.SocketEventManager.ConnectSocket += DoOpen;
                Mission.SocketEventManager.DisconnectSocket += DoClose;
                Mission.SocketEventManager.SocketConnected += TestSocket;
                Mission.SocketEventManager.OnConnectSocket();
            }
        }

        private void OnDisable()
        {
            if (On)
            {
                Mission.SocketEventManager.ConnectSocket -= DoOpen;
                Mission.SocketEventManager.DisconnectSocket -= DoClose;
                Mission.SocketEventManager.SocketConnected -= TestSocket;
            }

            DoClose(null, EventArgs.Empty);
        }

        private void OnDestroy()
        {
            DoClose(null, EventArgs.Empty);
        }

        private void Update()
        {
            if (Input.GetKeyDown("space"))
                if (socket != null)
                {
                    Debug.Log("Sending Message.");
                    socket.Emit("testee", "I fucking hate you");
                    Debug.Log("Message Sent");
                }
        }

        private void DoClose(object sender, EventArgs e)
        {
            if (socket == null) return;
            Debug.Log("Closing Socket");
            socket.Disconnect();
            Debug.Log("Socket Closed");
            socket = null;
        }

        private void DoOpen(object sender, EventArgs e)
        {
            Debug.Log("Trying to create Socket");
            if (socket != null) return;
            socket =
                IO.Socket(
                    "http://ec2-52-24-126-225.us-west-2.compute.amazonaws.com:81/socket.io");
            if (socket != null)
            {
                socket.On(Socket.EVENT_CONNECT,
                    () => { Debug.Log("Connected."); });
                Debug.Log("Trying to connect to socket.");

                // Add listener for test data.
                socket.On("tester", data => { Debug.Log(data); });
            }
            else
            {
                Debug.Log("Could not create Socket.");
            }
        }

        private void TestSocket(object sender, EventArgs e)
        {
            if (On)
                if (socket != null)
                {
                    Debug.Log("Sending Message.");
                    socket.Emit("testee", "I fucking hate you");
                    Debug.Log("Message Sent");
                }
        }
    }
}