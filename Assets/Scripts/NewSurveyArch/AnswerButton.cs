using System;
using System.Security.AccessControl;
using Mission;
using UnityEngine;
using UnityEngine.UI;

namespace NewSurveyArch
{
    /// <inheritdoc />
    /// <summary>
    ///     Deals with changing the question index
    /// </summary>
    public class AnswerButton : MonoBehaviour
    {

        private const string Begin = "Begin";
        private const string Next = "Next";
        private const string Continue = "Continue";
        private const string NotAsnwered = "Cannot go to next question until current question is answered.";
        
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
            ButtonEventManager.QuestionNotComplete += OnQuestionNotComplete;
            ButtonEventManager.BeginQuestion+= OnBeginQuestion;
            ButtonEventManager.NextQuestion += OnNextQuestion;
            ButtonEventManager.ContinueQuestion += OnContinueQuestion;
            EventManager.SurveyReady += OnSurveyReady;
            EventManager.SurveyComplete += OnSurveyComplete;
        }

        

        public void OnDisable()
        {
            ButtonEventManager.QuestionNotComplete -= OnQuestionNotComplete;
            ButtonEventManager.BeginQuestion -= OnBeginQuestion;
            ButtonEventManager.NextQuestion -= OnNextQuestion;
            ButtonEventManager.ContinueQuestion -= OnContinueQuestion;
            EventManager.SurveyReady -= OnSurveyReady;
            EventManager.SurveyComplete -= OnSurveyComplete;
        }

        private void OnSurveyReady(object sender, EventArgs e)
        {
            gameObject.GetComponent<Button>().enabled = true;
        }

        /// <summary>
        ///     Waits for case when button is clicked, but answer is not answered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnQuestionNotComplete(object sender, EventArgs e)
        {
            var text = gameObject.GetComponentInChildren<Text>();
            text.text= NotAsnwered + "\n" + Next;
        }

        /// <summary>
        ///     Change text to "Continue" when on last question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBeginQuestion(object sender, EventArgs e)
        {
            //
            gameObject.GetComponentInChildren<Text>().text = Begin;
        }

        /// <summary>
        ///     Change text to "Continue" when on last question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNextQuestion(object sender, EventArgs e)
        {
            gameObject.GetComponentInChildren<Text>().text = Next;
        }

        /// <summary>
        ///     Change text to "Begin" when on first question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContinueQuestion(object sender, EventArgs e)
        {
            gameObject.GetComponentInChildren<Text>().text = Continue;
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