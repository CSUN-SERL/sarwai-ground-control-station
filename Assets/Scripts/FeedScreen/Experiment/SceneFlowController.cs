using UnityEngine;
using UnityEngine.SceneManagement;

namespace FeedScreen.Experiment
{
    internal class SceneFlowController : MonoBehaviour
    {
        public const string ProctorSetup = "ProctorSetup";
        public const string Welcome = "Welcome";
        public const string GeneralSurvey = "NewSurvey";
        public const string TransparencyBrief = "TransparentBrief";
        public const string MissionScene = "MissionScene";
        public const string FinalScene = "FinalScene";
        public const string Error = "Error";

        public static SceneFlowController Instance;

        public static void LoadNextScene()
        {
            var scene = SceneManager.GetActiveScene();
            var currentScene = scene.name;

            Debug.Log(currentScene);
            switch (currentScene)
            {
                case Participant.Participant.ProctorSetup:
                    SceneManager.LoadScene(Participant.Participant.Welcome);
                    break;
                default:
                    {

                        switch (Participant.Participant.Instance.CurrentScene)
                        {
                            case -2:
                                SceneManager.LoadScene(Participant.Participant.FinalScene);
                                break;
                            case -1:
                                SceneManager.LoadScene(Participant.Participant.TransparencyBrief);
                                break;
                            case 0:
                                SceneManager.LoadScene(Participant.Participant.QueryScreen);
                                break;
                            default:
                                SceneManager.LoadScene(Participant.Participant.SurveyScene);
                                break;
                        }
                        break;
                    }
            }
        }

        public static void LoadErrorScene()
        {
            SceneManager.LoadScene(Participant.Participant.Error);
        }

        public void LoadNextSceneWrapper()
        {
            LoadNextScene();
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
