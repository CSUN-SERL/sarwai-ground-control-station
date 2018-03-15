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



        /// <summary>
        ///     Event occurs when user completes the survey.
        /// </summary>
        public static event EventHandler<EventArgs> PushSurvey;

        public static void OnPushSurvey()
        {
            var handler = PushSurvey;
            if (handler != null) handler(null, new EventArgs());
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
    }

}