using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace NewTransparencyArch
{
    /// <summary>
    ///     Instantiates a gameobject givea a question.
    /// </summary>
    public class GenerateSurveyQuestionPrefab : ScriptableObject
    {
        /// <summary>
        ///     Sets up the text for a question with a given gameobject and instantiates it.
        /// </summary>
        /// <param name="tempPrefab"></param>
        /// <param name="question"></param>
        /// <returns>returns an instantiated GameObject</returns>
        private static GameObject InstatiatePrefabAndPopulateAnswer(
            GameObject tempPrefab, string question)
        {
            var answerSelection = Instantiate(tempPrefab);
            answerSelection.transform.GetChild(0).GetChild(0)
                .GetComponent<Text>().text = question;
            return answerSelection;
        }


        /// <summary>
        ///     Populates the answers into the gameobject and sets up a inputfiled for answers with '@' character at the front.
        /// </summary>
        /// <param name="answerSelection"></param>
        /// <param name="answers"></param>
        private static void PopulateAnswers(Component answerSelection,
            IList<string> answers)
        {
            var firstAnswer = answerSelection.gameObject;
            firstAnswer.SetActive(false);
            foreach (var answer in answers)
            {
                var newAnswer = answer;
                var instance = Instantiate(firstAnswer);
                instance.SetActive(true);
                instance.transform.SetParent(firstAnswer.transform.parent);
                if (newAnswer[0] == '@')
                {
                    newAnswer = answer.Substring(1);
                    instance.GetComponent<InsertInputFieldInParentsParent>()
                        .SpawnInsertFieldOnTrue = true;
                }

                instance.transform.GetChild(0).GetComponent<Text>().text =
                    newAnswer;
            }
        }


        /// <summary>
        ///     Sets up a question with an <see cref="InputField" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        public static GameObject FreeResponseSetUp(
            SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(
                    Resources.Load<GameObject>("SurveyQuestion/FreeResponse"),
                    questionDetails.question_text);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with a <see cref="Dropdown" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        /// <remarks>ToList() is used because Unity's <see cref="Dropdown" /> does not accept Itterables.</remarks>
        public static GameObject MultipleSetup(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(
                    Resources.Load<GameObject>("SurveyQuestion/Multiple"),
                    questionDetails.question_text);
            var answerSelection = tempPrefab.transform.GetChild(1).GetChild(0)
                .GetChild(1);
            //Debug.Log(answerSelection.name + " is in MultipleSetup");
            PopulateAnswers(answerSelection,
                questionDetails.offered_answer_text);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with a several <see cref="Button" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        /// <remarks>ToList() is used because Unity's <see cref="Dropdown" /> does not accept Itterables.</remarks>
        public static GameObject ScalarSetup(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(
                    Resources.Load<GameObject>("SurveyQuestion/Scalar"),
                    questionDetails.question_text);
            var answerSelection = tempPrefab.transform.GetChild(1).GetChild(0)
                .GetChild(1);
            //Debug.Log(answerSelection.name + " is in ScalarSetup");
            PopulateAnswers(answerSelection,
                questionDetails.offered_answer_text);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with <see cref="Dropdown" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        /// <remarks>ToList() is used because Unity's <see cref="Dropdown" /> does not accept Itterables.</remarks>
        public static GameObject NumericSetup(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(
                    Resources.Load<GameObject>("SurveyQuestion/Numeric"),
                    questionDetails.question_text);
            var answerSelection = tempPrefab.transform.GetChild(1).GetChild(0)
                .GetChild(0);
            //Debug.Log(answerSelection.name);
            var answerList = new List<string>();
            foreach (var answer in questionDetails.offered_answer_text)
            {
                var rangeOrAnswer = Regex.Split(answer, @"-");
                if (rangeOrAnswer.Length > 1)
                {
                    int initialRange;
                    int.TryParse(rangeOrAnswer[0], out initialRange);
                    int finalRange;
                    int.TryParse(rangeOrAnswer[1], out finalRange);
                    for (var i = initialRange; i < finalRange; ++i)
                        answerList.Add(i.ToString());
                }
                else
                {
                    answerList.Add(answer);
                }
            }

            var dropdown = answerSelection.GetChild(0).GetChild(0)
                .GetComponent<Dropdown>();
            dropdown.AddOptions(answerList);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with a single question and no answers.
        /// </summary>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
        public static GameObject MessegeSetup(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(
                    Resources.Load<GameObject>("SurveyQuestion/Messege"),
                    questionDetails.question_text);

            return tempPrefab;
        }

        public static GameObject PickAllSetup(SurveyQuestion currentDetails)
        {
            var tempPrefab = MultipleSetup(currentDetails);
            Destroy(tempPrefab.transform.GetChild(1).GetChild(0).GetChild(0)
                .gameObject);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with <see cref="Slider" />
        /// </summary>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
        public static GameObject TLXSetup(
            SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(
                    Resources.Load<GameObject>("SurveyQuestion/TLX"),
                    questionDetails.question_text);

            //Sets the text on the right side of the scale.
            tempPrefab.transform.GetChild(1).GetChild(0)
                    .GetChild(0).GetChild(0).GetChild(0).GetChild(0)
                    .GetComponent<Text>().text =
                questionDetails.offered_answer_text[0];

            //Sets the text on the right side of the scale.
            tempPrefab.transform.GetChild(1).GetChild(0)
                    .GetChild(0).GetChild(0).GetChild(0).GetChild(1)
                    .GetComponent<Text>().text =
                questionDetails.offered_answer_text[1];
            return tempPrefab;
        }
    }
} //                Instantiate(Resources.Load("SurveyQuestion/Multiple") as GameObject);