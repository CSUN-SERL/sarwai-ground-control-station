using System;
using UnityEngine;

namespace NewSurveyArch
{
    public class SurveyPush : MonoBehaviour
    {
        public void OnEnable()
        {
            EventManager.FetchSurveyFromWeb += OnPush;
        }

        public void OnDisable()
        {
            EventManager.FetchSurveyFromWeb -= OnPush;
        }


        /// <summary>
        ///     Loading survey from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnPush(object sender, EventArgs e)
        {
            //TODO:load the survey onto game objects
            EventManager.OnPushedSurvey();
        }
    }
}