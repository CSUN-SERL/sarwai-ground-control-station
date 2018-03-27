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
    public class AnswerGatherer : MonoBehaviour
    {
        public static AnswerGatherer Instance;
        private int _surveyNumber;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public void GatherAnswers(ref List<GameObject> go,
            ref List<SurveyQuestion> surveyQuestionList, int tempSurveyNumber)
        {
            _surveyNumber = tempSurveyNumber;
            foreach (var g in go)
            {
                var temp = surveyQuestionList[int.Parse(g.name)];

                //Debug.Log(surveyQuestionList[int.Parse(g.name)].QuestionId +
                //" is question id");
                //Debug.Log(surveyQuestionList[int.Parse(g.name)].Type +
                //" is question type");

                switch (temp.type)
                {
                    case "FreeResponse":
                        FreeResponseGetAnswer(g, ref temp);
                        break;
                    case "Multiple":
                        MultipleGetAnswer(g, ref temp);
                        break;
                    case "Scalar":
                        ScalarGetAnswer(g, ref temp);
                        break;
                    case "Numerical":
                        NumericGetAnswer(g, ref temp);
                        break;
                    //special cases bellow
                    case "Debrief":
                        MultipleGetAnswer(g, ref temp);
                        //Debug.Log("Debrief");
                        StartCoroutine(UploadPreferredLvlOfAutonomy(temp));
                        break;

                    case "Intro":
                        continue;
                    case "Outro":
                        continue;


                    case "IfYesRespond":
                        IfYesRespondGetAnswer(g, ref temp);
                        break;
                    case "IfNoRespond":
                        IfNoRespondGetAnswer(g, ref temp);
                        break;
                    case "IfScalarLessThan3Respond":
                        IfScalarLessThan3RespondGetAnswer(g, ref temp);
                        break;
                    case "TLX":
                        ScaleGetAnswer(g, ref temp);
                        break;
                    case "PickAll":
                        PickAllGetAnswer(g, ref temp);
                        break;
                }


                //Debug.Log(surveyQuestionList[int.Parse(g.name)].SelectedAnswer +
                //          " is answer");
                UploadQuery(ref temp);

                //Debug.Log(currentDetails.QuestionString + " is question string");
                //Debug.Assert(false, "Question type does not exist");
            }

            //Debug.Log("End of Survey");
            //Will go back to the QueryScene  
            EventManager.OnEnd();
            SceneFlowController.LoadNextScene();
        }
        //tested}

        //tested
        private static void FreeResponseGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            var parent = go.transform.GetChild(1).GetChild(0).GetChild(0)
                .GetChild(2).GetComponent<Text>().text;
            questionDetails.SelectedAnswer = parent;
        }

        private static void PickAllGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            //Debug.Log(go.transform.GetChild(1).GetChild(0).name +
            //          " is in Multiple");
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            questionDetails.SelectedAnswer = "";
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                    questionDetails.SelectedAnswer += "|" +
                                                      questionDetails
                                                          .OfferedAnswerList[i];

            if (questionDetails.SelectedAnswer.Length > 1)
                questionDetails.SelectedAnswer =
                    questionDetails.SelectedAnswer.Substring(1);
        }

        //tested
        private static void MultipleGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            //Debug.Log(go.transform.GetChild(1).GetChild(0).name +
            //          " is in Multiple");
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            Debug.Log("Toggle count = "+ toggles.Count);
            for (var i = 0; i < toggles.Count; ++i)
            {
                Debug.Log(i + " = i,good");

                if (toggles[i].isOn)
                {
                    Debug.Log(i + " = i,good");
                    questionDetails.SelectedAnswer =
                        questionDetails.OfferedAnswerList[i];
                    return;
                }
            }

            questionDetails.SelectedAnswer = "Not Answered";
        }

        //tested
        private static void ScalarGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            //Debug.Log(go.transform.GetChild(1).GetChild(0).name +
            //          " is in Scalar");
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                {
                    //Debug.Log(i + " = i");
                    questionDetails.SelectedAnswer =
                        questionDetails.OfferedAnswerList[i];
                    return;
                }

            questionDetails.SelectedAnswer = "Not Answered";
        }

        private static void NumericGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            var parent = go.transform.GetChild(1).GetChild(0).GetChild(0)
                .GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text;
            questionDetails.SelectedAnswer = parent;
        }

        private static void IfYesRespondGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                {
                    questionDetails.SelectedAnswer =
                        questionDetails.OfferedAnswerList[i] +
                        (i == 0
                            ? " " + go.transform.GetChild(1).GetChild(1)
                                  .GetChild(2)
                                  .GetComponent<Text>().text
                            : "");
                    return;
                }

            questionDetails.SelectedAnswer = "Not Answered";
        }

        private static void IfNoRespondGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                {
                    questionDetails.SelectedAnswer =
                        questionDetails.OfferedAnswerList[i] +
                        (i == 1
                            ? " " + go.transform.GetChild(1).GetChild(1)
                                  .GetChild(2)
                                  .GetComponent<Text>().text
                            : "");
                    return;
                }

            questionDetails.SelectedAnswer = "Not Answered";
        }

        private static void IfScalarLessThan3RespondGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            var toggles = go.transform.GetChild(1).GetChild(0)
                .GetComponentsInChildren<Toggle>().ToList();
            for (var i = 0; i < toggles.Count; ++i)
                if (toggles[i].isOn)
                {
                    questionDetails.SelectedAnswer =
                        questionDetails.OfferedAnswerList[i] +
                        (i < 3
                            ? go.transform.GetChild(1).GetChild(1)
                                .GetComponent<InputField>().text
                            : "");
                    break;
                }
        }


        private static void ScaleGetAnswer(GameObject go,
            ref QuestionDetails questionDetails)
        {
            var rawValue =
                go.transform.GetChild(1).GetChild(0).GetChild(0)
                    .GetChild(0).GetComponent<Slider>().normalizedValue;
            var percent = 5 * Mathf.CeilToInt(rawValue * 100 / 5);
            questionDetails.SelectedAnswer = percent + "%";
        }

        private void UploadQuery(ref QuestionDetails questionDetails)
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
                questionDetails.QuestionId,
                questionDetails.OfferedAnswerId,
                questionDetails.SelectedAnswer,
                particiapantId);
            Debug.Log("Nope just a Tide Ad " + sqlQuery);
            StartCoroutine(UploadQueryEnumerator(sqlQuery));
        }

        private static IEnumerator UploadPreferredLvlOfAutonomy(
            QuestionDetails questionDetail)
        {
            var lvlOfAuto = 0;
            for (;
                lvlOfAuto < questionDetail.OfferedAnswerList.Count;
                ++lvlOfAuto)
                if (questionDetail.OfferedAnswerList[lvlOfAuto] ==
                    questionDetail.SelectedAnswer)
                    break;
            if (lvlOfAuto >= questionDetail.OfferedAnswerList.Count)
                lvlOfAuto = 0;
            var dic =
                new Dictionary<string, int>
                {
                    {"lvl_of_autonomy", lvlOfAuto},
                    {"question_id", int.Parse(questionDetail.QuestionId)},
                    {
                        "mission_id",
                        ParticipantBehavior.Participant.CurrentMission - 1
                    }
                };


            var jsonDic = JsonConvert.SerializeObject(dic, Formatting.Indented);

            var form = new WWWForm();
            form.AddField("json", jsonDic);
            using (var www =
                UnityWebRequest.Post(ServerURL.UPDATE_LVL_OF_AUTONOMY, form))
            {
                //Debug.Log(jsonDic);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                    Debug.Log(www.error);
                else
                    Debug.Log(www.downloadHandler.text + " is result");
            }
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