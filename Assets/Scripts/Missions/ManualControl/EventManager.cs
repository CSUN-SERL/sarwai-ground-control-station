using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace Mission.ManualControl
{
    /// <summary>
    ///     The manual control event manager.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        private static EventManager _eventManager;

        public static EventManager instance
        {
            get
            {
                {
                if (!_eventManager)
                    _eventManager =
                        FindObjectOfType(typeof(EventManager)) as
                            EventManager;

                    if (!_eventManager)
                        Debug.LogError(
                            "There needs to be one active EventManger script on a GameObject in your scene.");
                }

                return _eventManager;
            }
        }

        public static event EventHandler<IntEventArgs> ManualControlDisable;

        public static event EventHandler<IntEventArgs> ManualControlEnable;

        // public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;
        public static void OnManualControlDisable(int i)
        {
            var handler = ManualControlDisable;
            if (handler != null) handler(null, new IntEventArgs {intField = i});
        }

        // public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;
        public static void OnManualControlEnable(int i)
        {
            var handler = ManualControlEnable;
            if (handler != null) handler(null, new IntEventArgs {intField = i});
        }
    }
}