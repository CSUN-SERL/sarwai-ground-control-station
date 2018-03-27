using System;
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
            EventManager.NextQuestion -= NextQuestion;

        }

        private void StartSurvey(object sender, EventArgs e)
        {
            EventManager.SurveyReady -= StartSurvey;
            total = gameObject.transform.childCount - 1;
            Debug.Log("total = " + total);
            displayNumber = 0;
            if (total == 1)
                ButtonEventManager.OnContinueQuestion();
            else
                ButtonEventManager.OnBeginQuestion();
            if (displayNumber < total)
            {
                gameObject.transform.GetChild(displayNumber).gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("End of Questions");
                EventManager.OnSurveyComplete();
            }
        }

        private void NextQuestion(object sender, EventArgs e)
        {
            Debug.Log("NextQuestion");
             
            if (displayNumber < total)
            {
                ButtonEventManager.OnNextQuestion();
                gameObject.transform.GetChild(displayNumber).gameObject.SetActive(false);
                ++displayNumber;
                
                gameObject.transform.GetChild(displayNumber).gameObject.SetActive(true);
                if (displayNumber == total)
                {
                    ButtonEventManager.OnContinueQuestion();
                }
            }
            else
            {
                Debug.Log("End of Questions");
                EventManager.OnSurveyComplete();
            }
        }

        /// <summary>
        ///     Displays once the listener recieves the call.
        /// </summary>
        public override void DisplayOn()
        {
            gameObject.GetComponent<Image>().enabled = true;
        }

        /// <summary>
        ///     Stosp displaying once the listener recieves the call.
        /// </summary>
        public override void DisplayOff()
        {
            gameObject.GetComponent<Image>().enabled = false;
        }

        protected override void Upload()
        {
            throw new System.NotImplementedException();
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