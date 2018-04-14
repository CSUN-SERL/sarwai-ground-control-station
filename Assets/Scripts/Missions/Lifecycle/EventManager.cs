using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace Mission.Lifecycle
{
    public class EventManager : MonoBehaviour
    {

        /// <summary>
        /// Signals to load the mission metadata such as length, description, and id.
        /// </summary>
        public static event EventHandler<EventArgs> LoadMetaData;

        public static void OnLoadMetaData()
        {
            Debug.Log("Load Meta Data Event Called.");
            var handler = LoadMetaData;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        /// <summary>
        /// Signals when the mission metadata has been loaded.
        /// </summary>
        public static event EventHandler<MissionEventArgs> MetaDataLoaded;

        public static void OnMetaDataLoaded(int missionId,
            float missionLength, string missionBrief)
        {
            var handler = MetaDataLoaded;
            if (handler != null)
                handler(null, new MissionEventArgs
                {
                    MissionNumber = missionId,
                    MissionLength = missionLength,
                    MissionBrief = missionBrief
                });
        }


        /// <summary>
        /// Signals to start the initialization process.
        /// </summary>
        public static event EventHandler<IntEventArgs> Initialize;

        public static void OnInitialize(int missionId)
        {
            Debug.Log("Initialize Mission Event Called.");
            var handler = Initialize;
            if (handler != null)
                handler(null, new IntEventArgs {intField = missionId});
        }


        /// <summary>
        /// Signals that a mission has been initialized.
        /// </summary>
        public static event EventHandler<EventArgs> Initialized;

        public static void OnInitialized()
        {
            Debug.Log("Mission Initialized Event Called.");
            var handler = Initialized;
            if (handler != null) handler(null, new EventArgs());
        }


        /// <summary>
        /// When the mission has been initialized and the mission is ready to start.
        /// </summary>
        public static event EventHandler<EventArgs> Ready;

        public static void OnReady() {
            var handler = Ready;
            if (handler != null) handler(null, EventArgs.Empty);
        }


        /// <summary>
        /// When the participant starts the mission.
        /// </summary>
        public static event EventHandler<EventArgs> Start;

        public static void OnStart()
        {
            var handler = Start;
            if (handler != null) handler(null, EventArgs.Empty);
        }


        /// <summary>
        /// When the mission has been started.
        /// </summary>
        public static event EventHandler<EventArgs> Started;

        public static void OnStarted()
        {
            if (Started != null) Started(null, new EventArgs());
        }


        /// <summary>
        /// When the mission is completed.
        /// </summary>
        public static event EventHandler<EventArgs> Completed;

        public static void OnCompleted()
        {
            if (Completed != null) Completed(null, EventArgs.Empty);
        }

        /// <summary>
        /// Signals to shutdown the mision.
        /// </summary>
        public static event EventHandler<EventArgs> Stop;

        public static void OnClose()
        {
            var handler = Stop;
            if (handler != null) handler(null, EventArgs.Empty);
        }


        /// <summary>
        /// Signals when the mission has been shutdown.
        /// </summary>
        public static event EventHandler<EventArgs> Stopped;

        public static void OnStopped()
        {
            if (Stopped != null) Stopped(null, EventArgs.Empty);
        }
    }
}