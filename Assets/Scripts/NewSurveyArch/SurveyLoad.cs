using System;
using UnityEngine;

namespace NewSurveyArch
{
    /// <inheritdoc />
    /// <summary>
    ///     Responsible for sending SurveyManager the answer to question.
    /// </summary>
    /// <remarks>
    ///     Last Edited on 12/27/2017
    ///     Last Edited by Anton.
    /// </remarks>
    public class SurveyLoad : MonoBehaviour
    {
        public void OnEnable()
        {
            EventManager.FetchedSurvey += OnLoad;
        }

        public void OnDisable()
        {
            EventManager.FetchedSurvey -= OnLoad;
        }

        /// <summary>
        ///     Called when survey finished loading from web
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnLoad(object sender, SurveyEventArgs e)
        {
            var list = e.QuestionsList;
            //TODO:load the survey onto game objects
        }
        
    }
}