using UnityEngine;

namespace Menu_Navigation.Button_Logic
{
    internal class CameraSwitcher : MonoBehaviour
    {
        public Canvas Canvas_PreSurvey;
        public Canvas Canvas_Welcome;
        public Camera PreSurveyCamera;
        public Camera WelcomeCamera;

        public void EnableCameraPreSurvey()
        {
            WelcomeCamera.enabled = false;
            PreSurveyCamera.enabled = true;
            Canvas_Welcome.enabled = false;
            Canvas_PreSurvey.enabled = true;
        }
    }
}