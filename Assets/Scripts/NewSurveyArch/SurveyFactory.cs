using System.Collections.Generic;
using UnityEngine;

namespace NewSurveyArch
{
    /// <summary>
    ///     Coordinates the conversion of a list of questions as children of the gameobject this component is attached to.
    /// </summary>
    public class SurveyFactory : MonoBehaviour
    {
        public void OnEnable()
        {
            EventManager.FetchedSurvey += OnLoad;
        }

        public void OnDisable()
        {
            EventManager.FetchedSurvey -= OnLoad;
        }

        /// <summary>
        ///     Recieves a list which to load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoad(object sender, SurveyListEventArgs e)
        {
            //Debug.Log("Event started");
            Loading(e.QuestionsList);
        }

        /// <summary>
        ///     Converts of a list of questions as children of the gameobject this component is attached to.
        /// </summary>
        /// <param name="questionList"></param>
        private void Loading(List<SurveyQuestion> questionList)
        {
            var index = 0;
            foreach (var surveyQuestion in questionList)
            {
                var tempObject = SurveyQuestionToGameObject(surveyQuestion);
                tempObject.transform.SetParent(transform);
                tempObject.SetActive(false);
                ++index;
            }

            EventManager.OnSurveyReady();
        }

        /// <summary>
        ///     Figures out which prefab needs to be instantiated.
        ///     Asks the <see cref="GenerateSurveyQuestionPrefab" /> to instantiate given prefab
        /// </summary>
        /// <param name="currentDetails"></param>
        /// <returns></returns>
        private GameObject SurveyQuestionToGameObject(
            SurveyQuestion currentDetails)
        {
            //  Debug.Log(currentDetails.type);
            switch (currentDetails.type)
            {
                case "FreeResponse":
                    return GenerateSurveyQuestionPrefab.FreeResponseSetUp(
                        currentDetails);
                case "Multiple":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(
                        currentDetails);
                case "Scalar":
                    return GenerateSurveyQuestionPrefab.ScalarSetup(
                        currentDetails);
                case "Numerical":
                    return GenerateSurveyQuestionPrefab.NumericSetup(
                        currentDetails);

                //special cases bellow
                case "Intro":
                    return GenerateSurveyQuestionPrefab.MessegeSetup(
                        currentDetails);
                case "Outro":
                    return GenerateSurveyQuestionPrefab.MessegeSetup(
                        currentDetails);
                case "IfYesRespond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(
                        currentDetails);
                case "IfNoRespond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(
                        currentDetails);
                case "IfScalarLessThan3Respond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(
                        currentDetails);
                case "Scale":
                    return GenerateSurveyQuestionPrefab
                        .TLXSetup(currentDetails);
                case "TLX":
                    return GenerateSurveyQuestionPrefab
                        .TLXSetup(currentDetails);
                case "PickAll":
                    return GenerateSurveyQuestionPrefab.PickAllSetup(
                        currentDetails);
            }

            Debug.Log(string.Format("Question of type '{0}' does not exist",
                currentDetails.type));
            //In case a they type has never been implemented.
            return GenerateSurveyQuestionPrefab.FreeResponseSetUp(
                currentDetails);
        }
    }
}