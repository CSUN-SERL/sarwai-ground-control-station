using System;
using FeedScreen.Experiment;
using Mission.Lifecycle;
using Networking;
using NUnit.Framework.Constraints;
using Participant;
using UnityEngine;

namespace Mission
{
    public class MissionLifeCycleController : MonoBehaviour
    {
        public const string INITIALIZE_MISSION = "gcs-initialize-mission";
        public const string MISSION_INITIALIZED = "gcs-mission-initialized";

        public const string MISSION_READY = "gcs-mission-ready";

        public const string START_MISSION = "gcs-start-mission";
        public const string MISSION_STARTED = "gcs-mission-started";

        public const string MISSION_COMPLETED = "gcs-mission-completed";

        public const string STOP_MISSION = "gcs-stop-mission";
        public const string MISSION_STOPPED = "gcs-mission-stopped";

        //public const string CLOSE_MISSION = "gcs-close-mission";
        //public const string MISSION_CLOSED = "gcs-mission-closed";

        public static MissionLifeCycleController Instance;

        public bool Initialized { get; private set; }
        public bool Ready { get; private set; }
        public bool Started { get; private set; }
        public bool Running
        {
            get { return Started && Initialized && Ready; }
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);
        }

        private void OnEnable() {
            Lifecycle.EventManager.Initialize += InitializeMission;
            Lifecycle.EventManager.Initialized += OnInitialized;
            Lifecycle.EventManager.Ready += OnReady;
            Lifecycle.EventManager.Stopped += OnStopped;
        }

        private void OnDisable()
        {
            Lifecycle.EventManager.Initialize -= InitializeMission;
            Lifecycle.EventManager.Initialized -= OnInitialized;
            Lifecycle.EventManager.Ready -= OnReady;
            Lifecycle.EventManager.Stopped -= OnStopped;
        }

        /// <summary>
        /// Starts the initialization process for a mission.
        /// </summary>
        public static void InitializeMission(object sender, EventArgs e)
        {
            if (!GcsSocket.Alive)
            {
                Debug.Log("Error: Socket connection could not be established.");
                SceneFlowController.LoadErrorScene();
            };

            var initializeMissionParameters = string.Format("mission{0}-{1}-{2}-{3}",
                ParticipantBehavior.Participant.CurrentMission,
                ParticipantBehavior.Participant.Data.Adaptive ? "true" : "false",
                ParticipantBehavior.Participant.Data.Id,
                ParticipantBehavior.Participant.Data.Transparent ? "true" : "false");

            Debug.Log("Initializing Mission.  Parameters: " + initializeMissionParameters);

            GcsSocket.Emit(INITIALIZE_MISSION, initializeMissionParameters);

            Lifecycle.EventManager.OnInitialize(ParticipantBehavior.Participant.CurrentMission);
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            Instance.Initialized = true;
        }

        private void OnReady(object sender, EventArgs e)
        {
            Ready = true;
            Debug.Log("Ready");
        }

        public static void StartMission()
        {

            Instance.Started = true;

            if (!GcsSocket.Alive) {
                Debug.Log("Error: Socket connection could not be established.");
                SceneFlowController.LoadErrorScene();
            }

            GcsSocket.Emit(START_MISSION,
                ParticipantBehavior.Participant.CurrentMission);
            Started = true;
            Lifecycle.EventManager.OnStarted();
        }

        public static void CompleteMission()
        {
            Instance.Started = false;
            Instance.Initialized = false;
            Lifecycle.EventManager.OnCompleted();
        }

        public static void StopMission()
        {

            if (!GcsSocket.Alive) {
                Debug.Log("Error: Socket connection could not be established.");
                SceneFlowController.LoadErrorScene();
            }
            
            GcsSocket.Emit(STOP_MISSION,
                ParticipantBehavior.Participant.CurrentMission);
        }

        private void OnStopped(object sender, EventArgs e)
        {
            Debug.Log("Destroy");
            Destroy(this);
        }
    }
}