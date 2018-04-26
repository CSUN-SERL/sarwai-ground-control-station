using UnityEngine;
using UnityEngine.SceneManagement;

namespace FeedScreen.Experiment
{
    /// <summary>
    /// Scene flow controller controls the transitions from scene to scene in the ground control station.
    /// This class follows the singleton design pattern.
    /// </summary>
    internal class SceneFlowController : MonoBehaviour
    {

        // Names of all of the scenes.
        public const string ProctorSetup = "ProctorSetup";
        public const string Welcome = "Welcome";
        public const string GeneralSurvey = "NewSurvey";
        public const string TransparencyBrief = "TransparentBrief";
        public const string QueryScreen = "MissionScreen";
        public const string FinalScene = "FinalScene";
        public const string Error = "Error";


        // Instance for the singleton.
        public static SceneFlowController Instance;

        /// <summary>
        /// Makes sure that there is only one instance of this object at a time.
        /// </summary>
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        /// <summary>
        /// Makes sure that the scene flow controller does not get deleted when moving from scene to scene.
        /// </summary>
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }


        /// <summary>
        /// Given the current state of the ground control station, transition to the next scene.
        /// </summary>
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
                        Debug.Log(Participant.Participant.Instance.CurrentScene);
                    switch (Participant.Participant.Instance.CurrentScene)
                    {
                        case -2:
                            SceneManager.LoadScene(Participant.Participant.FinalScene);
                            break;
                        case -1:
                            SceneManager.LoadScene(Participant.Participant.TransparencyBrief);
                            break;
                        case 0:
                            SceneManager.LoadScene(Participant.Participant.MissionScreen);
                            break;
                        case 7:
                            SceneManager.LoadScene(Participant.Participant.PerformanceMetricsScene);
                            break;
                        default:
                            SceneManager.LoadScene(Participant.Participant.SurveyScene);
                            break;
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// The error scene is a catch-all for all critical system errors that are non recoverable.
        /// </summary>
        public static void LoadErrorScene()
        {
            SceneManager.LoadScene(Participant.Participant.Error);
        }


        /// <summary>
        /// This is a wrapper for gameobjects in unity to go to the next mission.
        /// </summary>
        public void LoadNextSceneWrapper()
        {
            LoadNextScene();
        }
    }
}
