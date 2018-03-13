using System;
using Mission.Lifecycle;
using UnityEngine;

namespace Mission
{
    /// <summary>
    ///     The Mission Conroller script is responsible for starting and stopping the mission.
    /// </summary>
    public class MissionTimer : MonoBehaviour
    {
        private static MissionTimer _instance;
        public static float StartTime;
        public static float CurrentTime;
        public static float MissionLength;
        public static bool Running;
        public static bool Paused;
        public static bool Completed;

        private void OnEnable()
        {
            EventManager.Started += OnStarted;
            EventManager.Completed += OnCompleted;
            EventManager.MetaDataLoaded += OnMetaDataLoaded;
        }

        private void OnDisable()
        {
            EventManager.Started -= OnStarted;
            EventManager.Completed -= OnCompleted;
            EventManager.MetaDataLoaded -= OnMetaDataLoaded;
        }

        private void Awake()
        {
            // Singleton.
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
                Destroy(gameObject);

            Time.timeScale = 0.0F;
        }


        // Update is called once per frame

        private void Update()
        {
            // Is mission time up and still running?
            CurrentTime = Time.timeSinceLevelLoad;
            if (MissionMetaData.MissionLength >= Time.timeSinceLevelLoad)
                return;
            if (!Running) return;
            EventManager.OnCompleted();
            MissionLifeCycleController.StopMission();
            Running = false;
        }

        public void OnMissionStarted()
        {
            Running = true;
            EventManager.OnStarted();
        }

        private void OnStarted(object sender, EventArgs eventArgs)
        {
            Paused = false;
            Running = true;
            Time.timeScale = 1.0F;
        }

        private void PauseMission()
        {
            Time.timeScale = 0.0F;
            Paused = true;
        }

        private void OnMetaDataLoaded(object sender, MissionEventArgs e)
        {
            MissionLength = e.MissionLength;
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            PauseMission();
            Running = false;
            Completed = true;
        }
    }
}