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
            {
                var tempObject = SurveyQuestionToGameObject(surveyQuestion);
                tempObject.transform.SetParent(transform);
            }
        }

        /// <summary>
        ///     Figures out which prefab needs to be instantiated. 
        /// Asks the <see cref="GenerateSurveyQuestionPrefab"/> to instantiate given prefab
        /// </summary>
        /// <param name="currentDetails"></param>
        /// <returns></returns>
        private GameObject SurveyQuestionToGameObject(SurveyQuestion currentDetails)
        {
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
                case "IfYesRespond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(currentDetails);
                case "IfNoRespond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(currentDetails);
                case "IfScalarLessThan3Respond":
                    return GenerateSurveyQuestionPrefab.MultipleSetup(currentDetails);
                case "Scale":
                    return GenerateSurveyQuestionPrefab.TLXSetup(currentDetails);
                case "TLX":
                    return GenerateSurveyQuestionPrefab.TLXSetup(currentDetails);
                case "PickAll":
                    return GenerateSurveyQuestionPrefab.PickAllSetup(currentDetails);
            }

            Debug.Log(string.Format("Question of type '{0}' does not exist",
                currentDetails.Type));

            return GenerateSurveyQuestionPrefab.FreeResponseSetUp(currentDetails);
        }
        
    }
}