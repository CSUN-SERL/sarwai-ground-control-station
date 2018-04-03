using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using Networking;
using Newtonsoft.Json.Linq;
using Participant;
using UnityEngine;
using UnityEngine.Networking;

namespace NewSurveyArch
{
    /// <inheritdoc />
    /// <summary>
    ///     Fetches the survey from the database.
    /// </summary>
    public class SurveyFetch : MonoBehaviour
    {
        /// <summary>
        ///     Sets up singleton.
        /// </summary>
        public static SurveyFetch Instance;

        /// <summary>
        ///     Auto sets up survey if participant has not been made.
        /// </summary>
        public int TestFetchingSurvey = 2;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
                
            //Test for creation of participant, else uses default.
            try
            {
                StartCoroutine(Fetching(ParticipantBehavior.Participant
                    .CurrentSurvey));
            }
            catch (NullReferenceException exception)
            {
                Debug.Log(exception.Message);
                Debug.Log(
                    string.Format(
                        "Pushing default survey {0}, because the participant has not been made.",
                        TestFetchingSurvey));
                StartCoroutine(Fetching(TestFetchingSurvey));
            }
        }

        public void OnEnable()
        {
            EventManager.FetchSurveyFromWeb += OnFetch;
        }

        public void OnDisable()
        {
            EventManager.FetchSurveyFromWeb -= OnFetch;
        }

        /// <summary>
        ///     Seperates the string by pipe.
        /// </summary>
        /// <param name="pipestuff"></param>
        /// <returns></returns>
        private static List<string> SeparatePipeInString(string pipestuff)
        {
            return Regex.Split(pipestuff, @"\|").ToList();
        }

        /// <summary>
        ///     Converter from JToken to question list.
        /// </summary>
        /// <param name="questionsJToken"></param>
        /// <returns></returns>
        private static List<SurveyQuestion> JTokenToQuestionDetailList(
            JToken questionsJToken)

        {
            Debug.Log(questionsJToken);
            var surveyList = new List<SurveyQuestion>();

            foreach (var question in questionsJToken)
            {
                Debug.Log(question.ToString());
                var q = SurveyQuestion.CreateFromJson(question);
                surveyList.Add(q);
            }

            Debug.Log("List begins");
            Debug.Log(surveyList);
            Debug.Log("List of size " + surveyList.Count);
            return surveyList;
        }

        /// <summary>
        ///     Fetches the json that consists of survey questions.
        /// </summary>
        /// <param name="surveyNumber"></param>
        /// <returns></returns>
        public IEnumerator Fetching(int surveyNumber)
        {
            var form = new WWWForm();
            form.AddField("survey_id", surveyNumber);


            using (var www = UnityWebRequest.Post(ServerURL.RETRIEVE_SURVEY,
                form))
            {
                Debug.Log("fetching...");
                var temp = www.SendWebRequest();
                while (!temp.isDone)
                {
                    Debug.Log("Download Stat: " + temp.progress);

                    //Wait each frame in each loop OR Unity would freeze
                    yield return null;
                }

                Debug.Log("fetching...breakpoint");
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    var result = JObject.Parse(www.downloadHandler.text);
                    Debug.Log(result["response"] + " is result");

                    if (result["response"].ToString() != "True")
                    {
                        Debug.Log(result["error"]);
                        Application.Quit();
                    }
                    else
                    {
                        var temp2 = JTokenToQuestionDetailList(result["data"]);
                        EventManager.OnFetchedSurvey(temp2);
                        Debug.Log("Event Called");
                    }
                }
            }
        }

        /// <summary>
        ///     Loading survey from database when Listener is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFetch(object sender, IntEventArgs e)
        {
            Debug.Log("FetchStarted");
            try
            {
                StartCoroutine(Fetching(ParticipantBehavior.Participant
                    .CurrentSurvey));
            }
            catch (NullReferenceException exception)
            {
                Debug.Log(exception.Message);
                Debug.Log(
                    "Using defaunlt survey 1, because the participant has not been made.");
                StartCoroutine(Fetching(TestFetchingSurvey));
            }
        }
    }
}