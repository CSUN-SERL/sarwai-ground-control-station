using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Networking;
using Participant;
using Tobii.Plugins;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NewSurveyArch
{
    /// <inheritdoc />
    /// <summary>
    ///     Pushes an answered survey question to the database.
    /// </summary>
    public class SurveyQuestionPush : MonoBehaviour
    {
        //Allows the bypass of questions for testing.

#if UNITY_EDITOR
        public static string NoAnswerMessege = "Answered";
#else
        public static string NoAnswerMessege = "Not Answered";
#endif
        public static string AnswerMessege = "Answered";

        /// <summary>
        ///     Locally stores the survey number for ease of use.
        /// </summary>
        private int _surveyNumber;

        /// <summary>
        ///     Locally stores the question List for reference.
        /// </summary>
        private List<SurveyQuestion> _surveyQuestionList;


        /// <summary>
        ///     Set up for singleton.
        /// </summary>
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
            EventManager.UploadQuestion += AttemptUpload;
        }

        private void AttemptUpload(object sender, UploadQuestionEventArgs e)
        {
            var messege = GatherAnswer(e.surveyQuestionObject,
                e.surveyQuestionIndex);
            Debug.Log(e.surveyQuestionIndex + " == " +
                      (_surveyQuestionList.Count - 2));
            if (e.surveyQuestionIndex == _surveyQuestionList.Count - 2)
                EventManager.OnPushedSurvey();
            EventManager.OnUploadedQuestion(messege);
        }

        private void OnDisable()
        {
            EventManager.FetchedSurvey -= StoreQuestionList;
            EventManager.UploadQuestion -= AttemptUpload;
        }


        /// <summary>
        ///     Converts any apostrophe given to a backquote to avoid mysql issues.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private static string ReplaceApostropy(string target)
        {
            return target.Replace("'", "`");
        }

        private void StoreQuestionList(object sender, SurveyListEventArgs e)
        {
            _surveyQuestionList = e.QuestionsList;
            try
            {
                _surveyNumber = ParticipantBehavior.Participant.CurrentSurvey;
            }
            catch (NullReferenceException exception)
            {
                Debug.Log(exception.Message);
                _surveyNumber = GameObject.Find("EventSystem")
                    .GetComponent<SurveyFetch>().TestFetchingSurvey;
                Debug.Log(
                    string.Format(
                        "Pushing default survey {0}, because the participant has not been made.",
                        _surveyNumber));
            }
        }

        /// <summary>
        ///     Get the answer based on type, and for uploading to database.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="questionNumber"></param>
        /// <returns></returns>
        public string GatherAnswer(GameObject g, int questionNumber)
        {
            Debug.Log(AnswerMessege + " = AnswerMessege");

            var temp = _surveyQuestionList[questionNumber];
            Debug.Log(temp.type + temp.question_text + " in answer gather");
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
        }

        /// <summary>
        ///     Fills out the offered_answer of the question for question type "FreeResponse", from the input field.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
        private string FreeResponseGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            var answer = go.transform.GetChild(1).GetChild(0).GetChild(0)
                .GetChild(2).GetComponent<Text>().text;

            if (answer.Length == 0) return NoAnswerMessege;

            questionDetails.offered_answer = ReplaceApostropy(answer);
            StartCoroutine(UploadQuery(questionDetails));
            return AnswerMessege;
        }

        /// <summary>
        ///     Fills out the offered_answer of the question for question type "PickAll", from all selected choice from a group of
        ///     <see cref="Toggle" />.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
        private string PickAllGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            questionDetails.offered_answer = "";
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                    questionDetails.offered_answer += "|" +
                                                      ReplaceApostropy(
                                                          questionDetails
                                                              .offered_answer_text
                                                              [
                                                                  i]);

            if (questionDetails.offered_answer.Length > 1)
            {
                questionDetails.offered_answer =
                    questionDetails.offered_answer.Substring(1);
                StartCoroutine(UploadQuery(questionDetails));
                return AnswerMessege;
            }

            return NoAnswerMessege;
        }

        /// <summary>
        ///     Fills out the offered_answer of the question for question type "Multiple", from the first selected choice from a
        ///     group of <see cref="Toggle" />.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
        private string MultipleGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            Debug.Log("Toggle count = " + toggles.Count);
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                {
                    if (questionDetails
                            .offered_answer_text[i][0] == '@')
                    {
                        var inputField = go.transform.GetChild(1).GetChild(1)
                            .GetChild(2).GetComponentInChildren<Text>().text;
                        Debug.Log(inputField);
                        //if (inputField == "")
                        //    break;
                        questionDetails.offered_answer =
                            ReplaceApostropy(questionDetails
                                .offered_answer_text[i]) + "|" + inputField;
                    }
                    else
                    {
                        questionDetails.offered_answer =
                            ReplaceApostropy(questionDetails
                                .offered_answer_text[i]);
                    }

                    
                    StartCoroutine(UploadQuery(questionDetails));
                    return AnswerMessege;
                }

            return NoAnswerMessege;
        }

        /// <summary>
        ///     Fills out the offered_answer of the question for question type "Scalar", from the first selected choice from a
        ///     group of <see cref="Toggle" />.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
        private string ScalarGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            for (var i = 0; i < toggles.Count; ++i)
                if(toggles[i].isOn)
            {
                if (questionDetails
                        .offered_answer_text[i][0] == '@')
                {
                    var inputField = go.transform.GetChild(1).GetChild(1)
                        .GetChild(2).GetComponentInChildren<Text>().text;
                    Debug.Log(inputField);
                    if (inputField == "")
                        break;
                    questionDetails.offered_answer =
                        ReplaceApostropy(questionDetails
                            .offered_answer_text[i]) + "|" + inputField;
                }
                else
                {
                    questionDetails.offered_answer =
                        ReplaceApostropy(questionDetails
                            .offered_answer_text[i]);
                }


                StartCoroutine(UploadQuery(questionDetails));
                return AnswerMessege;
            }

            return NoAnswerMessege;
        }

        /// <summary>
        ///     Fills out the offered_answer of the question for question type "Numeric", from the number given by
        ///     <see cref="Slider" />.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
        private string NumericGetAnswer(GameObject go,
            ref SurveyQuestion questionDetails)
        {
            var answer = go.transform.GetChild(1).GetChild(0).GetChild(0)
                .GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text;

            if (answer == questionDetails.offered_answer_text[0])
                return NoAnswerMessege;

            questionDetails.offered_answer = ReplaceApostropy(answer);
            StartCoroutine(UploadQuery(questionDetails));
            return AnswerMessege;
        }

        /// <summary>
        ///     Fills out the offered_answer of the question for question type "Scale", from the selected choice in a
        ///     <see cref="Dropdown" />.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Sets up a mysql query to upload to databse.
        /// </summary>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Uploads the query.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
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