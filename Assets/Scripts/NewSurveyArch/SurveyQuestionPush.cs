using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FeedScreen.Experiment;
using Networking;
using Newtonsoft.Json;
using Participant;
using Tobii.Plugins;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NewSurveyArch
{
    public class SurveyQuestionPush : MonoBehaviour
    {
        

        //T
#if UNITY_EDITOR
        public static string NoAnswerMessege = "Answered";
#else
        public static string NoAnswerMessege = "Not Answered";
#endif
        public static string AnswerMessege = "Answered";



        private int _surveyNumber;
        private List<SurveyQuestion> _surveyQuestionList;

        public static SurveyQuestionPush Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            EventManager.FetchedSurvey += StoreQuestionList;
        }

        private void OnDisable()
        {
            EventManager.FetchedSurvey -= StoreQuestionList;
        }

        private void StoreQuestionList(object sender, SurveyListEventArgs e)
        {
            _surveyQuestionList = e.QuestionsList;
        }

        public string GatherAnswer(GameObject g, int questionNumber)
        {
            Debug.Log(AnswerMessege + " = AnswerMessege");
            try
            {
                _surveyNumber = ParticipantBehavior.Participant.CurrentSurvey;
            }
            catch (System.NullReferenceException exception)
            {
                Debug.Log(exception.Message);
                Debug.Log(
                    "Pushing defaunlt survey 1, because the participant has not been made.");
                _surveyNumber = SurveyFetch.Instance.FetchSurvey;
            }

            var temp = _surveyQuestionList[questionNumber];

            //Debug.Log(surveyQuestionList[int.Parse(g.name)].QuestionId +
            //" is question id");
            //Debug.Log(surveyQuestionList[int.Parse(g.name)].Type +
            //" is question type");
            switch (temp.type)
            {
                case "FreeResponse":
                    return FreeResponseGetAnswer(g, ref temp);
                case "Multiple":
                    return MultipleGetAnswer(g, ref temp);

                case "Scalar":
                    return ScalarGetAnswer(g, ref temp);
                case "Numerical":
                    return NumericGetAnswer(g, ref temp);

                case "Intro":
                    return AnswerMessege;
                case "Outro":
                    return AnswerMessege;

                case "TLX":
                    return ScaleGetAnswer(g, ref temp);
                case "PickAll":
                    return PickAllGetAnswer(g, ref temp);
                default:
                    Debug.Log(string.Format(
                        "'{0}' does not exist please make sure the type is correct."));
                    return AnswerMessege;
            }


            //Debug.Log(surveyQuestionList[int.Parse(g.name)].SelectedAnswer +
            //          " is answer");
            

            //Debug.Log(currentDetails.QuestionString + " is question string");
            //Debug.Assert(false, "Question type does not exist");
        }
        //tested}

        //tested
        private string FreeResponseGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            var answer = go.transform.GetChild(1).GetChild(0).GetChild(0)
                .GetChild(2).GetComponent<Text>().text;
            if (answer.Length == 0)
                return NoAnswerMessege;
            else
            {
                questionDetails.offered_answer = answer;
                StartCoroutine(UploadQuery(questionDetails));
                return AnswerMessege;
            }

        }

        private string PickAllGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            //Debug.Log(go.transform.GetChild(1).GetChild(0).name +
            //          " is in Multiple");
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            questionDetails.offered_answer = "";
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                    questionDetails.offered_answer += "|" +
                                                      questionDetails
                                                          .offered_answer_text[
                                                              i];

            if (questionDetails.offered_answer.Length > 1)
            {


                questionDetails.offered_answer =
                    questionDetails.offered_answer.Substring(1);
                StartCoroutine(UploadQuery(questionDetails));
                return AnswerMessege;
            }

            return NoAnswerMessege;
        }

        //tested
        private string MultipleGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            //Debug.Log(go.transform.GetChild(1).GetChild(0).name +
            //          " is in Multiple");
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            Debug.Log("Toggle count = " + toggles.Count);
            for (var i = 0; i < toggles.Count; ++i)
            {
                Debug.Log(i + " = i,good");

                if (toggles[i].isOn)
                {
                    Debug.Log(i + " = i,good");
                    questionDetails.offered_answer =
                        questionDetails.offered_answer_text[i];
                    StartCoroutine(UploadQuery(questionDetails));
                    return AnswerMessege;
                }
            }

            return NoAnswerMessege;
        }

        //tested
        private string ScalarGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            //Debug.Log(go.transform.GetChild(1).GetChild(0).name +
            //          " is in Scalar");
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                {
                    //Debug.Log(i + " = i");
                    questionDetails.offered_answer =
                        questionDetails.offered_answer_text[i];
                    StartCoroutine(UploadQuery(questionDetails));
                    return AnswerMessege;
                }

            return NoAnswerMessege;
        }

        private string NumericGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            var answer = go.transform.GetChild(1).GetChild(0).GetChild(0)
                .GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text;

            if (answer == questionDetails.offered_answer_text[0])
                return NoAnswerMessege;
            else
            {
                questionDetails.offered_answer = answer;
                StartCoroutine(UploadQuery(questionDetails));
                return AnswerMessege;
            }
        }

        private string ScaleGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            var rawValue =
                go.transform.GetChild(1).GetChild(0).GetChild(0)
                    .GetChild(0).GetComponent<Slider>().normalizedValue;
            var percent = 5 * Mathf.CeilToInt(rawValue * 100 / 5);
            questionDetails.offered_answer = percent + "%";
            StartCoroutine(UploadQuery(questionDetails));
            return AnswerMessege;
        }

        private IEnumerator UploadQuery(SurveyQuestion questionDetails)
        {
            const string sql = "INSERT INTO dbsurveys.participant_result " +
                               "(survey_id, question_id, offered_answer_id, participant_answer_text, participant_id)" +
                               " VALUE ('{0}','{1}','{2}','{3}','{4}');";
            var particiapantId = 0;
            if (ParticipantBehavior.Instance != null)
                particiapantId = ParticipantBehavior.Participant.Data.Id;

            var sqlQuery = string.Format(
                sql,
                _surveyNumber,
                questionDetails.question_id,
                questionDetails.offered_answer_id,
                questionDetails.offered_answer,
                particiapantId);
            Debug.Log("Nope just a Tide Ad " + sqlQuery);
            StartCoroutine(UploadQueryEnumerator(sqlQuery));
            yield return null;
        }
        
        private static IEnumerator UploadQueryEnumerator(string sqlQuery)
        {
            var form = new WWWForm();
            form.AddField("sql", sqlQuery);
            form.AddField("database", "dbsurveys");
            using (var www = UnityWebRequest.Post(ServerURL.INSERT, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    //Debug.Log(www.error);
                }
                else
                {
                    var result = JSON.Parse(www.downloadHandler.text);
                    //Debug.Log(result + " is result");
                    if (result["failed"].AsBool) Application.Quit();
                }
            }
        }
    }
}