using System;
using Mission;
using Networking;
using Participant;
using UnityEngine;

namespace Tests
{
    public class MissionLifeCycleTest : MonoBehaviour
    {
        public bool Automated;

        public bool On;

        private void OnEnable()
        {
            Mission.SocketEventManager.SocketConnected += OnSocketConnected;
            Mission.Lifecycle.EventManager.MetaDataLoaded += OnMetaDataLoaded;
            Mission.Lifecycle.EventManager.Completed += OnCompleted;
            Mission.Lifecycle.EventManager.Stopped += OnStopped;

            if (!Automated || !On) return;
            Mission.SocketEventManager.OnConnectSocket();
        }

        private void OnDisable()
        {
            Mission.SocketEventManager.SocketConnected -= OnSocketConnected;
            Mission.Lifecycle.EventManager.MetaDataLoaded -= OnMetaDataLoaded;
            Mission.Lifecycle.EventManager.Completed -= OnCompleted;
            Mission.Lifecycle.EventManager.Stopped -= OnStopped;
        }

        private void OnSocketConnected(object sender, EventArgs e)
        {
            Mission.Lifecycle.EventManager.OnLoadMetaData();
        }

        private void OnMetaDataLoaded(object sender, MissionEventArgs e)
        {
            Mission.Lifecycle.EventManager.OnInitialize(ParticipantBehavior.Participant.CurrentMission);
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            Mission.Lifecycle.EventManager.OnClose();
        }

        private void OnStopped(object sender, EventArgs e)
        {
            Mission.SocketEventManager.OnDisconnectSocket();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!On) return;

            //Mission Ended
            if (Input.GetKeyDown("m"))
            {
                Debug.Log("connect-to-socket");
                Mission.SocketEventManager.OnConnectSocket();
            }

            //Initialize Mission
            if (Input.GetKeyDown("i"))
            {
                Debug.Log(Mission.MissionLifeCycleController.INITIALIZE_MISSION);
                MissionLifeCycleController.InitializeMission();
                Mission.Lifecycle.EventManager.OnInitialize(1);
            }

            //Mission Initialized
            if (Input.GetKeyDown("o"))
            {
                Debug.Log("gcs-mission-intitialized");
                Mission.Lifecycle.EventManager.OnInitialized();
            }

            //Start Mission
            if (Input.GetKeyDown("s"))
            {
                Debug.Log("gcs-start-mission");
                Mission.Lifecycle.EventManager.OnStart();
            }

            //Mission Started
            if (Input.GetKeyDown("d"))
            {
                Debug.Log("gcs-mission-started");
                Mission.Lifecycle.EventManager.OnStarted();
            }

            //Mission Completed
            if (Input.GetKeyDown("c"))
            {
                Debug.Log("gcs-mission-completed");
                Mission.Lifecycle.EventManager.OnCompleted();
            }

            //End Mission
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("gcs-close-mission");
                Mission.Lifecycle.EventManager.OnClose();
            }

            //Mission Ended
            if (Input.GetKeyDown("t"))
            {
                Debug.Log("gcs-stop-mission");
                MissionLifeCycleController.StopMission();
            }

            //Mission Ended
            if (Input.GetKeyDown("r"))
            {
                Debug.Log("gcs-mission-stopped");
                Mission.Lifecycle.EventManager.OnStopped();
            }
        }
    }
}