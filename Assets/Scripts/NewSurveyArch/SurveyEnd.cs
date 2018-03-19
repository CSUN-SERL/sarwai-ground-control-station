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