using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using LiveFeedScreen.ROSBridgeLib;
using LiveFeedScreen.ROSBridgeLib.std_msgs.std_msgs;
using Mission.Lifecycle;
using UnityEngine;

namespace Networking
{
    public class RosbridgeSocket : MonoBehaviour
    {
        public static RosbridgeSocket Instance;

        private ROSBridgeWebSocketConnection _rosA;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
        }

        private void OnEnable()
        {
            EventManager.Initialize += OnInitialize;
            EventManager.Completed += OnCompleted;
        }

        private void OnDisable()
        {
            EventManager.Initialize -= OnInitialize;
            EventManager.Completed -= OnCompleted;
        }

        private void OnInitialize(object sender, IntEventArgs e)
        {
            Connect();
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void Connect()
        {
            if (_rosA != null) return;
            Debug.Log("Rosbridge Connecting...");
            _rosA = new ROSBridgeWebSocketConnection(ServerURL.ROSBRIDGE_URL, ServerURL.ROSBRIDGE_PORT);
            _rosA.AddPublisher(typeof(CoffeePublisher));
            _rosA.Connect();
            Debug.Log("Rosbridge Connected.");
        }

        private void Disconnect()
        {
            if (_rosA == null) return;
            Debug.Log("Rosbridge Disconnecting...");
            _rosA.Disconnect();
            _rosA = null;
            Debug.Log("Rosbridge Disconnected.");
        }

        public static void Emit(string topic, string message)
        {
            Debug.Log(string.Format("RosBridge: Sending {0} on {1}", message, topic));
            var str = new StringMsg(message);
            Instance._rosA.Publish(topic, str);
            Instance._rosA.Render();
        }
    }
}