using UnityEngine;
using UnityEngine.UI;
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
            Debug.Log("Event started");
            var list = e.QuestionsList;
            int index = 0;
            foreach (var surveyQuestion in list)
            {
                var tempObject = SurveyQuestionToGameObject(surveyQuestion);
                Debug.Log(transform.name);
                tempObject.transform.SetParent(transform);
                tempObject.SetActive(false);
                tempObject.name = index.ToString();
                ++index;
            }
            EventManager.OnSurveyReady();
        }

        /// <summary>
        ///     Figures out which prefab needs to be instantiated. 
        /// Asks the <see cref="GenerateSurveyQuestionPrefab"/> to instantiate given prefab
        /// </summary>
        /// <param name="currentDetails"></param>
        /// <returns></returns>
        private GameObject SurveyQuestionToGameObject(SurveyQuestion currentDetails)
        {
            switch (currentDetails.type)
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
                currentDetails.type));

            return GenerateSurveyQuestionPrefab.FreeResponseSetUp(currentDetails);
        }
        
    }
}