using System;
using System.Security.AccessControl;
using Mission;
using UnityEngine;
using UnityEngine.UI;

namespace NewSurveyArch
{
    /// <summary>
    ///     Deals with changing the question index
    /// </summary>
    public class AnswerButton : MonoBehaviour
    {
        private Button _button;

        private const string Begin = "Begin";
        private const string Next = "Next";
        private const string Continue = "Continue";

        private void Awake()
        {
            _button = gameObject.GetComponentInChildren<Button>();
            //hides the button image
            _button.GetComponent<Text>().text = Begin;
            _button.enabled = false;
        }

        /// <summary>
        ///     For brevity, can be improved.
        ///     
        ///     Changes text of button to "Next" when a question is not the last
        /// nor first question.
        /// </summary>
        private void OnMouseDown()
        {
            _button.GetComponent<Text>().text = Next;
        }

        internal void OnEnable()
        {
            EventManager.SurveyReady += OnFirstQuestion;
            EventManager.LastQuestion += OnLastQuestion;
            EventManager.SurveyComplete += OnSurveyComplete;
        }

        public void OnDisable()
        {
            EventManager.SurveyReady -= OnFirstQuestion;
            EventManager.LastQuestion -= OnLastQuestion;
            EventManager.SurveyComplete -= OnSurveyComplete;
        }

        /// <summary>
        ///     Change text to "Continue" when on last question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLastQuestion(object sender, EventArgs e)
        {
            _button.GetComponent<Text>().text = Continue;
        }

        /// <summary>
        ///     Hides button to avoid multiple event listeners to be called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSurveyComplete(object sender, EventArgs e)
        {
            _button.enabled = false;
        }

        /// <summary>
        ///     Change text to "Begin" when on first question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFirstQuestion(object sender, EventArgs e)
        {
            _button.enabled = true;
        }
    }
}