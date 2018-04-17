using System;
using FeedScreen.Experiment;
using UnityEngine;

namespace NewTransparencyArch
{
    /// <summary>
    ///     Dicatates action based on end of survey.
    /// </summary>
    public class SurveyEnd : MonoBehaviour
    {
        private int events;

        public void OnEnable()
        {
            EventManager.PushedSurvey += OnPushed;
            EventManager.SurveyComplete += OnComplete;
        }

        public void OnDisable()
        {
            EventManager.PushedSurvey -= OnPushed;
            EventManager.SurveyComplete -= OnComplete;
        }
        
        /// <summary>
        ///     Singleton.
        /// </summary>
        public static SurveyEnd Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            events = 0;
                
        }


        /// <summary>
        ///     Marks pushed as true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPushed(object sender, EventArgs e)
        {
            Debug.Log("Survey was recognized as pushed by SurveyEnd");
            Done();
        }

        /// <summary>
        ///     Marks pushed as true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnComplete(object sender, EventArgs e)
        {
            Debug.Log("Survey was recognized as complete by SurveyEnd");
            Done();
        }

        private void Done()
        {
            ++events;
            if (events == 2 )
            {
                Debug.Log("Survey was ended by SurveyEnd");
                SceneFlowController.LoadNextScene();
            }
                
        }
            
    }
}