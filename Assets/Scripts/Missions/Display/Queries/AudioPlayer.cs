using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace Mission.Display.Queries
{
    public class AudioPlayer : MonoBehaviour
    {
        public AudioSource Source;

        private void OnEnable()
        {
            DisplayEventManager.DisplayAudioClip += OnDisplayAudioClip;
            DisplayEventManager.ClearDisplay += OnClearDisplay;
        }

        private void OnDisable()
        {
            DisplayEventManager.DisplayAudioClip -= OnDisplayAudioClip;
            DisplayEventManager.ClearDisplay -= OnClearDisplay;
        }

        private void OnDisplayAudioClip(object sender, AudioEventArgs e)
        {
            Source = FindObjectOfType<AudioSource>();
            if (Source == null)
            {
                Source = gameObject.AddComponent<AudioSource>();
            }
            Source.clip = e.Clip;
            Source.Play();
            Debug.Log(Source.clip.length);
        }

        private void OnClearDisplay(object sender, EventArgs e)
        {
            if (Source == null) return;
            Source.Stop();
        }
    }
}