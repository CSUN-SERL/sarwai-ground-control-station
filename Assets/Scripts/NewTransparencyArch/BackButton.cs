using System;
using System.Security.AccessControl;
using Mission;
using UnityEngine;
using UnityEngine.UI;

namespace NewTransparencyArch
{
    /// <inheritdoc />
    /// <summary>
    ///     Deals with changing the question index
    /// </summary>
    public class BackButton : MonoBehaviour
    {

        private const string Back = "Back";
        
        /// <summary>
        ///     For brevity, can be improved.
        ///     
        ///     Changes text of button to "Next" when a question is not the last
        /// nor first question.
        /// </summary>
        private void OnMouseDown()
        {
            Debug.Log("MouseDown");
            EventManager.OnNextQuestion();
        }

        internal void OnEnable()
        {
            ButtonEventManager.BeginQuestion+= OnBeginQuestion;
            ButtonEventManager.NextQuestion += OnNextQuestion;
            EventManager.SurveyComplete += OnSurveyComplete;
        }

        

        public void OnDisable()
        {
            ButtonEventManager.BeginQuestion -= OnBeginQuestion;
            ButtonEventManager.NextQuestion -= OnNextQuestion;
            EventManager.SurveyComplete -= OnSurveyComplete;
        }
        
        /// <summary>
        ///    Hides button when on first question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBeginQuestion(object sender, EventArgs e)
        {
            gameObject.GetComponent<Button>().enabled = false;
        }

        /// <summary>
        ///     Makes button uppear whenever there is a next question, aka not first question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNextQuestion(object sender, EventArgs e)
        {
            gameObject.GetComponent<Button>().enabled = true;
        }

        /// <summary>
        ///     Hides button to avoid multiple event listeners to be called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSurveyComplete(object sender, EventArgs e)
        {
            gameObject.SetActive(false);
        }

        
    }
}