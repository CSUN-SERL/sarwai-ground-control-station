using System;
using FeedScreen.Experiment;
using UnityEngine;

namespace NewSurveyArch
{
    public class SurveyEnd : MonoBehaviour
    {
        public void OnEnable()
        {
            EventManager.PushedSurvey += OnEnd;
        }

        public void OnDisable()
        {
            EventManager.PushedSurvey -= OnEnd;
        }

        public static SurveyEnd Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }


        /// <summary>
        ///     Ends the scene
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnEnd(object sender, EventArgs e)
        {
            SceneFlowController.LoadNextScene();
        }
    }
}