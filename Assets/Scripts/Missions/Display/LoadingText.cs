using System;
using System.Collections;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;
using UnityEngine.UI;
using EventManager = Mission.Lifecycle.EventManager;

namespace Mission.Display
{
    public class LoadingText : MonoBehaviour
    {
        private Queue _doOnMainThread;
        private Coroutine _loadingCoroutine;
        private string _status = "Loading";

        public Text Text;

        private void OnEnable()
        {
            _doOnMainThread = new Queue();
            Lifecycle.EventManager.Initialize += OnInitialize;
            Lifecycle.EventManager.Initialized += OnInitialized;
            _loadingCoroutine = StartCoroutine(DotAnimation());
        }

        private void OnDisable()
        {
            _doOnMainThread = null;
            Lifecycle.EventManager.Initialize -= OnInitialize;
            Lifecycle.EventManager.Initialized -= OnInitialized;
            _loadingCoroutine = null;
        }

        private void Update()
        {
            while (_doOnMainThread.Count > 0)
            {
                _doOnMainThread.Dequeue();
                StopCoroutine(_loadingCoroutine);
                Text.text = "Press Start to begin mission.";
            }
        }

        private void OnInitialize(object sender, IntEventArgs e)
        {
            _status = "Initializing Mission";
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            _doOnMainThread.Enqueue(0);
        }

        private IEnumerator DotAnimation()
        {
            var dotCounter = 1;
            string dots;

            while (true)
            {
                dotCounter += 1;
                if (dotCounter > 3) dotCounter = 1;
                dots = new string('.', dotCounter);
                Text.text = _status + dots;
                yield return new WaitForSecondsRealtime(0.5F);
            }
        }
    }
}