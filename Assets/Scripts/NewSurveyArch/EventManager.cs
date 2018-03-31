using System;
using System.Collections.Generic;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace NewSurveyArch
{
    public class SurveyListEventArgs : System.EventArgs
    {
        public List<SurveyQuestion> QuestionsList;
    }
    public class SurveyObjectEventArgs : System.EventArgs
    {
        public GameObject AnswerBox;
    }

    public class EventManager : MonoBehaviour
    {
        /// <summary>
        ///     Event occurs when there is a need to fetch a survey
        /// </summary>
        public static event EventHandler<IntEventArgs> FetchSurveyFromWeb;

        public static void OnFetchSurveyFromWeb(int i)
        {
            var handler = FetchSurveyFromWeb;
            if (handler != null) handler(null, new IntEventArgs {intField = i});
        }


        /// <summary>
        ///     Event occurs when <see cref="SurveyFetch"/> is done fetching survey data.
        /// </summary>
        public static event EventHandler<SurveyListEventArgs> FetchedSurvey;

        public static void OnFetchedSurvey(List<SurveyQuestion> questionList)
        {
            var handler = FetchedSurvey;
            if(handler != null) handler(null,new SurveyListEventArgs { QuestionsList = questionList });
        }

        public static event EventHandler<EventArgs> NextQuestion;
        public void OnNextQuestionWrapper()
        {
            EventManager.OnNextQuestion();
        }

        public static void OnNextQuestion()
        {
            var handler = NextQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        /// <summary>
        ///     Event occurs when survey is pushed.
        /// </summary>
        public static event EventHandler<EventArgs> PushedSurvey;

        public static void OnPushedSurvey()
        {
            var handler = PushedSurvey;
            if (handler != null) handler(null, new EventArgs());
        }

        public static event EventHandler<EventArgs> SurveyReady;

        public static void OnSurveyReady()
        {
            var handler = SurveyReady;
            if (handler != null) handler(null, new EventArgs());
        }
        
        /// <summary>
        ///     Event occurs when user completes the survey.
        /// </summary>
        public static event EventHandler<SurveyObjectEventArgs> SurveyComplete;

        public static void OnSurveyComplete(GameObject AnswerBox)
        {
            var handler = SurveyComplete;
            if (handler != null) handler(null, new SurveyObjectEventArgs{AnswerBox = AnswerBox});
        }

    }

}