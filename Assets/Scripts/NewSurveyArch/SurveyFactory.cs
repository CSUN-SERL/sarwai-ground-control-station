using UnityEngine;

namespace NewSurveyArch
{
    public class SurveyFactory : MonoBehaviour
    {
        private readonly string location = "Prefab/SurveyQuestion/";

        public void OnEnable()
        {
            EventManager.FetchedSurvey += OnLoad;
        }

        public void OnDisable()
        {
            EventManager.FetchedSurvey -= OnLoad;
        }

        /// <summary>
        ///     Called when survey finished loading from web
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoad(object sender, SurveyEventArgs e)
        {
            var list = e.QuestionsList;
            foreach (var surveyQuestion in list)
                MakeQuestionGameObjectAsChild(surveyQuestion);
        }

        private GameObject SurveyQuestionToGameObject(SurveyQuestion currentDetails)
        {
            //Debug.Log(currentDetails.Type);
            switch (currentDetails.Type)
            {
                case "FreeResponse":
                    return GenerateSurveyQuestionPrefab.FreeResponseSetUp(currentDetails);
                case "Multiple":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(currentDetails);
                case "Scalar":
                    return GenerateSurveyQuestionPrefab.ScalarSetup(currentDetails);
                case "Numerical":
                    return GenerateSurveyQuestionPrefab.NumericSetup(currentDetails);
                //special cases bellow

                case "Intro":
                    return GenerateSurveyQuestionPrefab.MessegeSetup(currentDetails);
                case "Outro":
                    return GenerateSurveyQuestionPrefab.MessegeSetup(currentDetails);
                case "Debrief":
                    return GenerateSurveyQuestionPrefab.DebriefSetup(currentDetails);
                case "IfYesRespond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(currentDetails);
                case "IfNoRespond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(currentDetails);
                case "IfScalarLessThan3Respond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(currentDetails);
                case "Scale":
                    return GenerateSurveyQuestionPrefab.ScaleSetup(currentDetails);
                case "TLX":
                    return GenerateSurveyQuestionPrefab.ScaleSetup(currentDetails);
                case "PickAll":
                    return GenerateSurveyQuestionPrefab.PickAllSetup(currentDetails);
            }

            Debug.Log(string.Format("Question of type '{0}' does not exist",
                currentDetails.Type));

            return GenerateSurveyQuestionPrefab.FreeResponseSetUp(currentDetails);
        }

        /// <summary>
        ///     Adds a survey question to survey container and sets it as the child of this gameobject.
        /// </summary>
        /// <param name="question"></param>
        public void MakeQuestionGameObjectAsChild(SurveyQuestion question)
        {
            var locationOfPrefab = location + question.Type;
            var instance =
                Instantiate(Resources.Load<GameObject>(locationOfPrefab));
            instance.transform.SetParent(transform);
            instance.GetComponent<MessegeContainer>().Init(question);
        }
    }
}