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
    public class SurveyFetch : MonoBehaviour
    {
        public void Awake()
        {
            StartCoroutine(Fetching(ParticipantBehavior.Participant.CurrentSurvey));
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
        ///     Converter for JToken.
        /// </summary>
        /// <param name="questionsJToken"></param>
        /// <returns></returns>
        private static List<SurveyQuestion> JTokenToQuestionDetailList(
            JToken questionsJToken)

        {
            Debug.Log(questionsJToken);
            Debug.Log(questionsJToken[0]);
            var surveyList = new List<SurveyQuestion>();

            foreach (var question in questionsJToken)
            {
                //var tempList =
                //    ScriptableObject.CreateInstance<SurveyQuestion>();
                Debug.Log(question.ToString());
                var q = SurveyQuestion.CreateFromJson(question);
                surveyList.Add(q);
                /*
                tempList.QuestionId = question["question_id"].ToString();
                tempList.QuestionType = question["type"].ToString();
                tempList.QuestionString = question["question_text"].ToString();
                tempList.OfferedAnswerId =
                    question["offered_answer_id"].ToString();

                tempList.OfferedAnswerList =
                    SeparatePipeInString(question["offered_answer_text"]
                        .ToString());

                surveyList.Add(tempList);
                Debug.Log(string.Format(
                    "QuestionId: {0}\nQuestionType{1}\nQuestionString: {2},OfferedId: {3}\n",
                    tempList.QuestionId, tempList.QuestionType,
                    tempList.QuestionString, tempList.OfferedAnswerId));
                    */
            }

            Debug.Log("List begins");
            Debug.Log(surveyList);
            Debug.Log("List of size" + surveyList.Count);
            return surveyList;
        }


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
                        Debug.Log("hello");
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
            StartCoroutine(Fetching(ParticipantBehavior.Participant
                .CurrentSurvey));
        }
    }
}