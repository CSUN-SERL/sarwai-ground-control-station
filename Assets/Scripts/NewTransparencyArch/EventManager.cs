using System;
using System.Collections.Generic;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using Mission;
using UnityEngine;

namespace NewTransparencyArch
{
    /// <summary>
    ///     Info needed to upload question.
    /// </summary>
    public class UploadQuestionEventArgs : EventArgs
    {
        public GameObject surveyQuestionObject;
        public int surveyQuestionIndex;
    }

    /// <summary>
    ///     Allows you to pass the list of question for a survey.
    /// </summary>
    public class SurveyListEventArgs : EventArgs
    {
        public List<SurveyQuestion> QuestionsList;
    }

    /// <summary>
    ///     Allows you to pass the Gameobject whose children are gameobjects made from the list of questions made by the
    ///     factory.
    /// </summary>
    public class SurveyObjectEventArgs : EventArgs
    {
        public GameObject AnswerBox;
    }

    /// <summary>
    ///     Handles events that coordinate a survey from start to finish.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        /// <summary>
        ///     Event occurs when there is a need to fetch a survey.
        /// </summary>
        public static event EventHandler<IntEventArgs> FetchSurveyFromWeb;

        public static void OnFetchSurveyFromWeb(int i)
        {
            var handler = FetchSurveyFromWeb;
            if (handler != null) handler(null, new IntEventArgs {intField = i});
        }
        
        /// <summary>
        ///     Event occurs when <see cref="SurveyFetch" /> is done fetching survey data.
        /// </summary>
        public static event EventHandler<SurveyListEventArgs> FetchedSurvey;

        public static void OnFetchedSurvey(List<SurveyQuestion> questionList)
        {
            var handler = FetchedSurvey;
            if (handler != null)
                handler(null,
                    new SurveyListEventArgs {QuestionsList = questionList});
        }

        /// <summary>
        ///     Wrapper for dynamic calls to NextQuestion.
        /// </summary>
        public void OnNextQuestionWrapper()
        {
            OnNextQuestion();
        }

        /// <summary>
        ///     Event occurs when a new question needs to be displayed.
        /// </summary>
        public static event EventHandler<EventArgs> NextQuestion;
               
        public static void OnNextQuestion()
        {
            var handler = NextQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        public static event EventHandler<UploadQuestionEventArgs> UploadQuestion;

        /// <summary>
        ///     Event occurs when a question needs to be uploaded.
        /// </summary>
        public static void OnUploadQuestion(GameObject g, int qNumber)
        {
            var handler = UploadQuestion;
            if (handler != null) handler(null, new UploadQuestionEventArgs { surveyQuestionObject = g, surveyQuestionIndex = qNumber });
        }

        public static event EventHandler<StringEventArgs> UploadedQuestion;

        /// <summary>
        ///     Event occurs when a question needs to be uploaded.
        /// </summary>
        public static void OnUploadedQuestion(string response)
        {
            var handler = UploadedQuestion;
            if (handler != null) handler(null, new StringEventArgs { StringArgs = response });
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

        /// <summary>
        ///     Event occurs when the gameobjects have been made.
        /// </summary>
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
            if (handler != null)
                handler(null,
                    new SurveyObjectEventArgs {AnswerBox = AnswerBox});
        }
    }
}