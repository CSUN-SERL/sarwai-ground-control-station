using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace NewSurveyArch
{

    /// <summary>
    /// TODO:add start time, display time, 
    /// </summary>
    public abstract class QuestionFab : MonoBehaviour
    {
        private Image _imageComponent;
        public Text Question;

        protected void Awake()
        {
            _imageComponent = gameObject.GetComponentInChildren<Image>();
            _imageComponent.enabled = false;
        }

        private void Start()
        {
            ChangeGameObjectValues();
        }

        internal void OnEnable()
        {
            EventManager.NextQuestion += UpdateDisplay;
        }

        public void OnDisable()
        {
            EventManager.NextQuestion -= UpdateDisplay;
        }

        private void OnDestroy()
        {
            UploadInfo();
        }

        public abstract void ChangeGameObjectValues();

        public abstract void UpdateDisplay(object sender, EventArgs e);

        public abstract void UploadInfo();
    }
}