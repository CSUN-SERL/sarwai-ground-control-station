using System;
using FeedScreen.Experiment;
using Participant;
using UnityEngine;
using EventManager = Mission.Lifecycle.EventManager;

namespace Mission
{
    /// <summary>
    ///     The Mission loader Class is responsible for loading the mission metadata for a specific mission
    ///     once the socket connection has been established.  Once the mission metadata is loaded, this class
    ///     will raise the MetaDataLoaded Event.
    /// </summary>
    public class MissionLoader : MonoBehaviour
    {
        /// <summary>
        ///     Enable the listeners for socket connection.
        /// </summary>
        private void OnEnable()
        {
            Lifecycle.EventManager.MetaDataLoaded += OnMetaDataLoaded;
            Lifecycle.EventManager.Completed += OnCompleted;
            Lifecycle.EventManager.Stopped += OnStopped;
        }

        /// <summary>
        ///     Remove the listener for socket connection.
        /// </summary>
        private void OnDisable()
        {
            Lifecycle.EventManager.MetaDataLoaded -= OnMetaDataLoaded;
            Lifecycle.EventManager.Completed -= OnCompleted;
            Lifecycle.EventManager.Stopped -= OnStopped;
        }

        public void EndMission()
        {
            Lifecycle.EventManager.OnClose();
        }

        private void OnMetaDataLoaded(object sender, MissionEventArgs e)
        {
            Lifecycle.EventManager.OnInitialize(ParticipantBehavior.Participant.CurrentMission);
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            Lifecycle.EventManager.OnClose();
        }

        private void OnStopped(object sender, EventArgs e)
        {
            SceneFlowController.Instance.LoadNextSceneWrapper();
        }
    }
}