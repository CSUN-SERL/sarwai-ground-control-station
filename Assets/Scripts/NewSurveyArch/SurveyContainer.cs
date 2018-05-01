using System;
using FeedScreen.Experiment;
using Mission;
using UnityEngine;

namespace NewSurveyArch
{
    /// <summary>
    ///     Manager for gameobjects that conatain survey questions.
    /// </summary>
    public class SurveyContainer : QuestionContainer
    {
        /// <summary>
        ///     Number on which the question is on.
        /// </summary>
        public int DisplayNumber;

        /// <summary>
        ///     Total number of questions.
        /// </summary>
        public int Total;


        private void OnEnable()
        {
            EventManager.SurveyReady += StartSurvey;
            EventManager.NextQuestion += NextQuestion;
            EventManager.UploadedQuestion += Uploaded;
        }

        private void Uploaded(object sender, StringEventArgs e)
        {
            UploadedMessege(e.StringArgs);
        }

        private void OnDisable()
        {
            EventManager.SurveyReady -= StartSurvey;
            EventManager.NextQuestion -= NextQuestion;
            EventManager.UploadedQuestion -= Uploaded;
        }

        /// <summary>
        ///     Starts the visibility of the first question and the button correlated with going to next question.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartSurvey(object sender, EventArgs e)
        {
            EventManager.SurveyReady -= StartSurvey;
            Total = gameObject.transform.childCount - 1;
            DisplayNumber = 0;
            ButtonEventManager.OnBeginQuestion();
            DisplayOn();
        }

        /// <summary>
        ///     Stops the visibility of current question. Starts the visibility of next question.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextQuestion(object sender, EventArgs e)
        {
            Debug.Log("NextQuestion");
            if (DisplayNumber < Total)
            {
                Upload();
            }
            else
            {
                Debug.Log("End of Questions");
                EventManager.OnSurveyComplete(gameObject);
                //SceneFlowController.LoadNextScene();
            }
        }

        /// <summary>
        ///     Displays current questinon.
        /// </summary>
        public override void DisplayOn()
        {
            gameObject.transform.GetChild(DisplayNumber).gameObject
                .SetActive(true);
            if (DisplayNumber == Total) ButtonEventManager.OnContinueQuestion();
        }

        /// <summary>
        ///     Stops displaying current question.
        /// </summary>
        public override void DisplayOff()
        {
            ButtonEventManager.OnNextQuestion();
            gameObject.transform.GetChild(DisplayNumber).gameObject
                .SetActive(false);
        }


        /// <summary>
        ///     Uploads the current question via <see cref="SurveyQuestionPush" />
        /// </summary>
        protected override void Upload()
        {
            var currentChildQuestion =
                gameObject.transform.GetChild(DisplayNumber);
            if (currentChildQuestion.childCount > 1)
            {
                Debug.Log(currentChildQuestion.childCount);
                Debug.Log(currentChildQuestion.name);
                EventManager.OnUploadQuestion(
                        currentChildQuestion.gameObject, DisplayNumber);                
            }
            else
            {
                UploadedMessege(SurveyQuestionPush.AnswerMessege);
            }
        }

        private void UploadedMessege(string messege)
        {
            if (messege == SurveyQuestionPush.AnswerMessege)
            {
                Debug.Log("Question answered.");
                DisplayOff();
                ++DisplayNumber;
                DisplayOn();
            }
            else if (messege == SurveyQuestionPush.NoAnswerMessege)
            {
                Debug.Log("Question not answered");
                ButtonEventManager.OnQuestionNotComplete();
            }
        }
    }
}