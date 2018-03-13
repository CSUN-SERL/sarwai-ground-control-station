using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace Mission.Lifecycle
{
    public class EventManager : MonoBehaviour
    {
        public static event EventHandler<IntEventArgs> Initialize;

        public static void OnInitialize(int missionId)
        {
            Debug.Log("Initialize Mission Event Called.");
            var handler = Initialize;
            if (handler != null)
                handler(null, new IntEventArgs {intField = missionId});
        }

        public static event EventHandler<EventArgs> Initialized;

        public static void OnInitialized()
        {
            Debug.Log("Mission Initialized Event Called.");
            var handler = Initialized;
            if (handler != null) handler(null, new EventArgs());
        }

        public static event EventHandler<EventArgs> LoadMetaData;

        public static void OnLoadMetaData()
        {
            Debug.Log("Load Meta Data Event Called.");
            var handler = LoadMetaData;
            if (handler != null) handler(null, EventArgs.Empty);
        }

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

        public static event EventHandler<EventArgs> Loaded;

        public static void OnLoaded()
        {
            var handler = Loaded;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> Start;

        public static void OnStart()
        {
            var handler = Start;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> Started;

        public static void OnStarted()
        {
            if (Started != null) Started(null, new EventArgs());
        }

        public static event EventHandler<EventArgs> Completed;

        public static void OnCompleted()
        {
            if (Completed != null) Completed(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> Stop;

        public static void OnClose()
        {
            var handler = Stop;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> Stopped;

        public static void OnStopped()
        {
            if (Stopped != null) Stopped(null, EventArgs.Empty);
        }
    }
}