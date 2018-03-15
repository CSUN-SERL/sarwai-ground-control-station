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