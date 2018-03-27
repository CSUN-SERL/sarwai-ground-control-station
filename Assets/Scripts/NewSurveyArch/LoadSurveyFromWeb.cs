using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Networking;
using Newtonsoft.Json.Linq;
using Survey;
using UnityEngine;
using UnityEngine.Networking;

namespace NewSurveyArch
{
    public class LoadSurveyFromWeb : ScriptableObject
    {
        public bool Loading;

        public List<SurveyQuestion> SurveyList { get; private set; }


        private void Start()
        {
            Loading = true;
        }

        private static List<string> SeparatePipeInString(string pipestuff)
        {
            return Regex.Split(pipestuff, @"\|").ToList();
        }


        private void JsonNodeToListList(JToken questionsJToken)
        {
            Debug.Log(questionsJToken);
            Debug.Log(questionsJToken[0]);
            SurveyList = new List<SurveyQuestion>();
            foreach (var question in questionsJToken)
            {
                var surveyQuestion = SurveyQuestion.Init(
                    text: question["question_text"].ToString(),
                    type: question["type"].ToString(),
                    offeredAnswerList: SeparatePipeInString(
                        question["offered_answer_text"].ToString()),
                    offeredAnswerId: question["offered_answer_id"].ToString(),
                    questionId: question["question_id"].ToString());
                
                SurveyList.Add(surveyQuestion);
                Debug.Log(string.Format(
                    "QuestionId: {0}\nQuestionType{1}\nQuestionString: {2},OfferedId: {3}\n",
                    surveyQuestion.question_id, surveyQuestion.type,
                    surveyQuestion.question_text, surveyQuestion.offered_answer_id));
            }

            Debug.Log("List begins");
            Debug.Log(SurveyList);
            Debug.Log("List ends");
            Loading = false;
        }
        /*
        private void String2DArrayToListList(string[][] data)
        {
            Debug.Log(data);
            Debug.Log(data[0]);
            SurveyList = new List<SurveyQuestion>();
            for (var i = 0; i < data.Length; ++i)
            {
                var tempList =
                    ScriptableObject.CreateInstance<QuestionDetails>();
                switch (data[i].Length)
                {
                    case 4:
                    {
                        tempList.OfferedAnswerList =
                            SeparatePipeInString(data[i][3]);
                        goto case 3;
                    }
                    case 3:
                    {
                        tempList.QuestionString = data[i][2];
                        goto case 2;
                    }
                    case 2:
                    {
                        tempList.QuestionType = data[i][1];
                        Debug.Log(tempList.QuestionType +
                                  " is in Load survey, i = " + i);
                        goto case 1;
                    }
                    case 1:
                    {
                        tempList.QuestionId = data[i][0];
                        break;
                    }
                }


                SurveyList.Add(tempList);
            }

            Debug.Log("List begins");
            Debug.Log(SurveyList);
            Debug.Log("List ends");
            Loading = false;
        }
        */
        public IEnumerator LoadSurveyEnumerator(int surveyNumber)
        {
            var form = new WWWForm();
            form.AddField("survey_id", surveyNumber);

            using (var www = UnityWebRequest.Post(ServerURL.RETRIEVE_SURVEY,
                form)) {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    /*
                    Debug.Log(www.error);
                    var result = new[]
                    {
                        new[]
                        {
                            "0", "Numeric",
                            "This is a dummy Numeric question because internet is not working.",
                            "8-100"
                        },
                        new[]
                        {
                            "1", "Multiple",
                            "This is a dummy Multiple question because internet is not working.",
                            "Yes|@No"
                        },
                        new[]
                        {
                            "2", "Multiple",
                            "This is a dummy Multiple question because internet is not working.",
                            "@Yes|No"
                        },
                        new[]
                        {
                            "3", "Scalar",
                            "This is a dummy Scalar question because internet is not working.",
                            "@one|@two|@three|four|five"
                        },
                        new[]
                        {
                            "4", "PickAll",
                            "This is a dummy PickAll question because internet is not working.",
                            "cool1|coool2|cool3|coool4|cool5|coool6"
                        }
                    };
                    String2DArrayToListList(result);
                }
                /*
                 * case "Numeric":
                        NumericGetAnswer(g, ref temp);
                        break;
                    //special cases bellow
                    case "IfYesRespond":
                        IfYesRespondGetAnswer(g, ref temp);
                        break;
                    case "IfNoRespond":
                        IfYesRespondGetAnswer(g, ref temp);
                        break;
                    case "IfScalarLessThan3Respond":
                        IfScalarLessThan3RespondGetAnswer(g, ref temp);
                        break;
                    case "Scale":
                        ScaleGetAnswer(g, ref temp);
                        break;
                        
                 */
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
                        JsonNodeToListList(result["data"]);
                    }
                }
            }
        }
    }
}