using System;
using FeedScreen.Experiment;
using Mission.Lifecycle;
using Networking;
using Participant;
using UnityEngine;
using EventManager = Mission.Lifecycle.EventManager;

namespace Mission
{
    public class MissionLifeCycleController : MonoBehaviour
    {
        public const string INITIALIZE_MISSION = "gcs-initialize-mission";
        public const string MISSION_INITIALIZED = "gcs-mission-initialized";

        public const string START_MISSION = "gcs-start-mission";
        public const string MISSION_STARTED = "gcs-mission-started";

        public const string MISSION_COMPLETED = "gcs-mission-completed";

        public const string STOP_MISSION = "gcs-stop-mission";
        public const string MISSION_STOPPED = "gcs-mission-stopped";

        //public const string CLOSE_MISSION = "gcs-close-mission";
        //public const string MISSION_CLOSED = "gcs-mission-closed";

        public static MissionLifeCycleController Instance;
        public bool Initialized { get; private set; }

        public bool Started { get; private set; }
        public bool Completed { get; private set; }

        public bool Running
        {
            get { return Started && !Completed; }
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);

            InitializeMission();
        }

        private void OnEnable()
        {
            EventManager.Initialized += OnInitialized;
            //EventManager.Completed += OnCompleted;
            EventManager.Stopped += OnStopped;
        }

        private void OnDisable()
        {
            EventManager.Initialized -= OnInitialized;
            //EventManager.Completed -= OnCompleted;
            EventManager.Stopped -= OnStopped;
        }

        /// <summary>
        /// Starts the initialization process for a mission.
        /// </summary>
        public static void InitializeMission()
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
            Initialized = true;
        }

        public static void StartMission()
        {

            if (!GcsSocket.Alive) {
                Debug.Log("Error: Socket connection could not be established.");
                SceneFlowController.LoadErrorScene();
            }

            GcsSocket.Emit(START_MISSION,
                ParticipantBehavior.Participant.CurrentMission);
            Lifecycle.EventManager.OnStarted();
        }

        public static void CompleteMission()
        {
            EventManager.OnCompleted();
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