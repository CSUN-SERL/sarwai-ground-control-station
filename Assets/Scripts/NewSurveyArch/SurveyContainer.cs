using System;
using FeedScreen.Experiment;
using Survey;
using UnityEngine;
using UnityEngine.UI;

namespace NewSurveyArch
{

    public class SurveyContainer : QuestionContainer
    {
        public int displayNumber;
        public int total;
        /*
        protected override void Download()
        {
        }

        protected override void Upload()
        {
        }*/
        private SurveyQuestion _question;

        // Use this for initialization
        void Awake()
        {
            displayNumber = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            EventManager.SurveyReady += StartSurvey;

            EventManager.NextQuestion += NextQuestion;
        }
        private void OnDisable()
        {
            EventManager.SurveyReady -= StartSurvey;
            EventManager.NextQuestion -= NextQuestion;

        }

        private void StartSurvey(object sender, EventArgs e)
        {
            EventManager.SurveyReady -= StartSurvey;
            total = gameObject.transform.childCount - 1;
            Debug.Log("total = " + total);
            displayNumber = 0;
            ButtonEventManager.OnBeginQuestion();
            DisplayOn();
        }

        private void NextQuestion(object sender, EventArgs e)
        {
            Debug.Log("NextQuestion");
            if (displayNumber < total)
            {
                Upload();   
            }
            else
            {
                Debug.Log("End of Questions");
                EventManager.OnSurveyComplete(gameObject);
                SceneFlowController.LoadNextScene();
            }
        }

        /// <summary>
        ///     Displays once the listener recieves the call.
        /// </summary>
        public override void DisplayOn()
        {
            gameObject.transform.GetChild(displayNumber).gameObject.SetActive(true);
            if (displayNumber == total)
            {
                ButtonEventManager.OnContinueQuestion();
            }
        }

        /// <summary>
        ///     Stosp displaying once the listener recieves the call.
        /// </summary>
        public override void DisplayOff()
        {
            ButtonEventManager.OnNextQuestion();
            gameObject.transform.GetChild(displayNumber).gameObject.SetActive(false);
            ++displayNumber;
        }

        protected override void Upload()
        {
            var currentChildQuestion = gameObject.transform.GetChild(displayNumber);
            if (currentChildQuestion.childCount > 1)
            {
                Debug.Log(currentChildQuestion.childCount);
                Debug.Log(currentChildQuestion.name);
                var messege = SurveyQuestionPush.Instance.GatherAnswer(currentChildQuestion.gameObject, displayNumber);
                if (messege == SurveyQuestionPush.AnswerMessege)
                {
                    Debug.Log("Question answerd.");
                    DisplayOff();
                    DisplayOn();
                }
                else if (messege == SurveyQuestionPush.NoAnswerMessege)
                {
                    Debug.Log("Question not answered");
                    ButtonEventManager.OnQuestionNotComplete();
                    return;

                }
            }
            else
            {
                DisplayOff();
                DisplayOn();
            }
        }


        /// <summary>
        ///     Stores question locally.
        /// </summary>
        /// <param name="question"></param>
        public void Init(SurveyQuestion question)
        {
            _question = question;
        }
    }
}