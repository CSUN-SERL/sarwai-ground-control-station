using System;
using System.Collections;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Mission
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
            Lifecycle.EventManager.Ready += OnReady;
            _loadingCoroutine = StartCoroutine(DotAnimation());
        }

        void Start()
        {
            Text = gameObject.GetComponent<Text>();
        }

        private void OnDisable()
        {
            _doOnMainThread = null;
            Lifecycle.EventManager.Initialize -= OnInitialize;
            Lifecycle.EventManager.Ready -= OnReady;
            _loadingCoroutine = null;
        }

        private void Update()
        {
            while (_doOnMainThread.Count > 0)
            {
                _doOnMainThread.Dequeue();
                StopCoroutine(_loadingCoroutine);
                Text.text = _status;
            }
        }

        private void OnInitialize(object sender, IntEventArgs e)
        {
            _status = "Initializing Mission";
        }

        private void OnReady(object sender, EventArgs e)
        {
            _status = "Mission is ready to begin, Please Press Start.";
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