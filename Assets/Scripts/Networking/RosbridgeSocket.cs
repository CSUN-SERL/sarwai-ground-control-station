using System;
using System.Collections;
using System.Net;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using LiveFeedScreen.ROSBridgeLib;
using LiveFeedScreen.ROSBridgeLib.std_msgs.std_msgs;
using Mission;
using Mission.Lifecycle;
using UnityEngine;

namespace Networking
{
    public class RosbridgeSocket
    {
        public static RosbridgeSocket Instance;

        public bool Connected { get; set; }

        private ROSBridgeWebSocketConnection _rosA;

        private IPEndPoint _endPoint;


        public RosbridgeSocket(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
        }

        //private void Awake()
        //{
        //    if (Instance == null)
        //    {
        //        Instance = this;
        //    }
        //    else if (Instance != this) Destroy(gameObject);
        //}

        //private void OnEnable()
        //{
        //    Mission.Lifecycle.EventManager.Initialize += OnInitialize;
        //    Mission.Lifecycle.EventManager.Completed += OnCompleted;
        //}

        //private void OnDisable()
        //{
        //    Mission.Lifecycle.EventManager.Initialize -= OnInitialize;
        //    Mission.Lifecycle.EventManager.Completed -= OnCompleted;
        //}

        //IEnumerator EnsureConnection()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            if (!Connected && MissionLifeCycleController.Instance.Running)
        //            {
        //                ConnectToSocket();
        //            }
        //        } catch(NullReferenceException e)
        //        {
                    
        //        }
               
        //        yield return new WaitForSecondsRealtime(1);
        //    }
        //}

        //private void OnInitialize(object sender, IntEventArgs e)
        //{
        //    ConnectToSocket();
        //}

        //private void OnCompleted(object sender, EventArgs e)
        //{
        //    Disconnect();
        //}

        public void ConnectToSocket()
        {
            if (_rosA != null) return;
            Debug.Log("Rosbridge Connecting...");
            _rosA = new ROSBridgeWebSocketConnection(_endPoint.Address.ToString(), _endPoint.Port);
            _rosA.AddPublisher(typeof(CoffeePublisher));
            _rosA.Connect();

            Debug.Log("Rosbridge Connected.");
        }

        public void Disconnect()
        {
            if (_rosA == null) return;
            Debug.Log("Rosbridge Disconnecting...");
            _rosA.Disconnect();
            _rosA = null;
            Debug.Log("Rosbridge Disconnected.");
        }

        public static void Emit(string topic, string message)
        {
            if (Instance._rosA == null)
            {
                Instance.ConnectToSocket();
            }

            Debug.Log(string.Format("RosBridge: Sending {0} on {1}", message, topic));
            var str = new StringMsg(message);
            Instance._rosA.Publish(topic, str);
            Instance._rosA.Render();
        }
    }
}