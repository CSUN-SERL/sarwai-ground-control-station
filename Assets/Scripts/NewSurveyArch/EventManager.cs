using System;
using System.Collections.Generic;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace NewSurveyArch
{
    public class SurveyEventArgs : System.EventArgs
    {
        public List<SurveyQuestion> QuestionsList;
    }

    public class EventManager : MonoBehaviour
    {
        /// <summary>
        ///     Event occurs when there is a need to fetch a survey
        /// </summary>
        public static event EventHandler<IntEventArgs> FetchSurvey;

        public static void OnFetchSurveyFromWeb(int i)
        {
            var handler = FetchSurvey;
            if (handler != null) handler(null, new IntEventArgs {intField = i});
        }


        /// <summary>
        ///     Event occurs when <see cref="SurveyFetch"/> is done fetching survey data.
        /// </summary>
        public static event EventHandler<SurveyEventArgs> FetchedSurvey;

        public static void OnFetchedSurvey(List<SurveyQuestion> questionList)
        {
            var handler = FetchedSurvey;
            if(handler != null) handler(null,new SurveyEventArgs {QuestionsList = questionList });
        }

        public static event EventHandler<IntEventArgs> ChangeQuestion;

        public static void OnChangeQuestion(int i)
        {
            var handler = ChangeQuestion;
            if (handler != null) handler(null, new IntEventArgs{ intField = i });
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

        public static event EventHandler<EventArgs> LastQuestion;

        public static void OnLastQuestion()
        {
            var handler = LastQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        /// <summary>
        ///     Event occurs when user completes the survey.
        /// </summary>
        public static event EventHandler<EventArgs> SurveyComplete;

        public static void OnSurveyComplete()
        {
            var handler = SurveyComplete;
            if (handler != null) handler(null, new EventArgs());
        }

    }

}