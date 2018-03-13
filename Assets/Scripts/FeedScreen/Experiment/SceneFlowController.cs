using Participant;
using UnityEngine;
using UnityEngine.SceneManagement;
using Participant = Participant.Participant;

namespace FeedScreen.Experiment
{
    internal class SceneFlowController : MonoBehaviour
    {
        public const string ProctorSetup = "ProctorSetup";
        public const string Welcome = "Welcome";
        public const string GeneralSurvey = "GeneralSurvey";
        public const string TransparencyBrief = "TransparentBrief";
        public const string QueryScreen = "QueryScreen";
        public const string FinalScene = "FinalScene";
        public const string Error = "Error";

        public static SceneFlowController Instance;
        private static int _flowNumber;

        public static void LoadNextScene()
        {
            var scene = SceneManager.GetActiveScene();
            var currentScene = scene.name;

            Debug.Log(currentScene);
            switch (currentScene)
            {
                case ProctorSetup:
                    _flowNumber = 0;
                    SceneManager.LoadScene(Welcome);
                    break;
                default:
                    //if (Participant.Instance.isDone)
                    //SceneManager.LoadScene("FinalScene");
                    //else //SceneManager.LoadScene(QueryScreen)
                {

                    Debug.Log(ParticipantBehavior.Participant.Data.Id);
                    Debug.Log(ParticipantBehavior.Participant.Data.Group);
                    Debug.Log(ParticipantBehavior.Participant.CurrentMission);
                    Debug.Log(ParticipantBehavior.Participant.CurrentSurvey);
                        ++_flowNumber;
                    if (ParticipantBehavior.Participant.Data.Group == 1)
                        AdaptiveTransparent();
                    else if (ParticipantBehavior.Participant.Data.Group == 2)
                        // TODO Add transparency to nonadaptive.
                        NonAdaptive();
                    else if (ParticipantBehavior.Participant.Data.Group == 3)
                        Adaptive();
                    else
                        NonAdaptive();
                }
                    break;
            }
        }

        public static void LoadErrorScene()
        {
            SceneManager.LoadScene(Error);
        }

        public static void AdaptiveTransparent()
        {
            Debug.Log(_flowNumber + " = flowNumber");
            switch (_flowNumber)
            {
                case 1:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 2:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 3:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 1
                case 4:
                    Debug.Log("SceneFlowController started Mission 1");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 5:
                    Debug.Log(
                        "SceneFlowController started Debrief 1, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 6:
                    Debug.Log(
                        "SceneFlowController started Debrief 1, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 7:
                    Debug.Log(
                        "SceneFlowController started Debrief 1, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 2
                case 8:
                    Debug.Log(
                        "SceneFlowController started Mission 2,Trasparency Brief");
                    SceneManager.LoadScene(TransparencyBrief);
                    break;
                case 9:
                    Debug.Log("SceneFlowController started Mission 2");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 10:
                    Debug.Log(
                        "SceneFlowController started Debrief 2, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 11:
                    Debug.Log(
                        "SceneFlowController started Debrief 2, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 12:
                    Debug.Log(
                        "SceneFlowController started Debrief 2, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 3
                case 13:
                    Debug.Log(
                        "SceneFlowController started Mission 3,Trasparency Brief");
                    SceneManager.LoadScene(TransparencyBrief);
                    break;
                case 14:
                    Debug.Log("SceneFlowController started Mission 3");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 15:
                    Debug.Log(
                        "SceneFlowController started Debrief 3, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 16:
                    Debug.Log(
                        "SceneFlowController started Debrief 3, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 17:
                    Debug.Log(
                        "SceneFlowController started Debrief 3, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 4
                case 18:
                    Debug.Log(
                        "SceneFlowController started Mission 4,Trasparency Brief");
                    SceneManager.LoadScene(TransparencyBrief);
                    break;
                case 19:
                    Debug.Log("SceneFlowController started Mission 4");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 20:
                    Debug.Log(
                        "SceneFlowController started Debrief 4, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 21:
                    Debug.Log(
                        "SceneFlowController started Debrief 4, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 22:
                    Debug.Log(
                        "SceneFlowController started Debrief 4, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 5
                case 23:
                    Debug.Log(
                        "SceneFlowController started Mission 5,Trasparency Brief");
                    SceneManager.LoadScene(TransparencyBrief);
                    break;
                case 24:
                    Debug.Log("SceneFlowController started Mission 5");
                    SceneManager.LoadScene(QueryScreen);
                    break;

                case 25:
                    Debug.Log(
                        "SceneFlowController started Debrief 5, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 26:
                    Debug.Log(
                        "SceneFlowController started Debrief 5, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                case 27:
                    Debug.Log(
                        "SceneFlowController started Post-Mission Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                case 28:
                    Debug.Log(
                        "SceneFlowController started Post-Mission Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                default:
                    Debug.Log("ScenFlowController started Final Scene");
                    SceneManager.LoadScene(FinalScene);
                    break;
            }
        }

        public static void Adaptive()
        {
            Debug.Log(_flowNumber + " = flowNumber");
            switch (_flowNumber)
            {
                case 1:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 2:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 3:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 1
                case 4:
                    Debug.Log("SceneFlowController started Mission 1");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 5:
                    Debug.Log(
                        "SceneFlowController started Debrief 1, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 6:
                    Debug.Log(
                        "SceneFlowController started Debrief 1, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 7:
                    Debug.Log(
                        "SceneFlowController started Debrief 1, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;


                //Mission 2   
                case 8:
                    Debug.Log("SceneFlowController started Mission 2");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 9:
                    Debug.Log(
                        "SceneFlowController started Debrief 2, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 10:
                    Debug.Log(
                        "SceneFlowController started Debrief 2, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 11:
                    Debug.Log(
                        "SceneFlowController started Debrief 2, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;


                //Mission 3
                case 12:
                    Debug.Log("SceneFlowController started Mission 3");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 13:
                    Debug.Log(
                        "SceneFlowController started Debrief 3, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 14:
                    Debug.Log(
                        "SceneFlowController started Debrief 3, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 15:
                    Debug.Log(
                        "SceneFlowController started Debrief 3, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;


                //Mission 4  
                case 16:
                    Debug.Log("SceneFlowController started Mission 4");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 17:
                    Debug.Log(
                        "SceneFlowController started Debrief 4, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 18:
                    Debug.Log(
                        "SceneFlowController started Debrief 4, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 19:
                    Debug.Log(
                        "SceneFlowController started Debrief 4, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;


                //Mission 5
                case 20:
                    Debug.Log("SceneFlowController started Mission 5");
                    SceneManager.LoadScene(QueryScreen);
                    break;

                    break;
                case 21:
                    Debug.Log(
                        "SceneFlowController started Debrief 5, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 22:
                    Debug.Log(
                        "SceneFlowController started Debrief 5, Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;


                case 23:
                    Debug.Log(
                        "SceneFlowController started Post-Mission Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                case 24:
                    Debug.Log(
                        "SceneFlowController started Post-Mission Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                default:
                    Debug.Log("ScenFlowController started Final Scene");
                    SceneManager.LoadScene(FinalScene);
                    break;
            }
        }

        public void LoadNextSceneWrapper()
        {
            LoadNextScene();
        }

        public static void NonAdaptive()
        {
            Debug.Log(_flowNumber + " = flowNumber");
            switch (_flowNumber)
            {
                case 1:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 2:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 3:
                    Debug.Log(
                        "SceneFlowController started Pre-Mission Survey 3");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 1
                case 4:
                    Debug.Log("SceneFlowController started Mission 1");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 5:
                    Debug.Log(
                        "SceneFlowController started Debrief 1, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 6:
                    Debug.Log(
                        "SceneFlowController started Debrief 1, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 2   
                case 7:
                    Debug.Log("SceneFlowController started Mission 2");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 8:
                    Debug.Log(
                        "SceneFlowController started Debrief 2, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 9:
                    Debug.Log(
                        "SceneFlowController started Debrief 2, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 3
                case 10:
                    Debug.Log("SceneFlowController started Mission 3");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 11:
                    Debug.Log(
                        "SceneFlowController started Debrief 3, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 12:
                    Debug.Log(
                        "SceneFlowController started Debrief 3, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 4  
                case 13:
                    Debug.Log("SceneFlowController started Mission 4");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 14:
                    Debug.Log(
                        "SceneFlowController started Debrief 4, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 15:
                    Debug.Log(
                        "SceneFlowController started Debrief 4, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                //Mission 5
                case 16:
                    Debug.Log("SceneFlowController started Mission 5");
                    SceneManager.LoadScene(QueryScreen);
                    break;
                case 17:
                    Debug.Log(
                        "SceneFlowController started Debrief 5, Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                case 18:
                    Debug.Log(
                        "SceneFlowController started Debrief 5, Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                case 19:
                    Debug.Log(
                        "SceneFlowController started Post-Mission Survey 1");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;

                case 20:
                    Debug.Log(
                        "SceneFlowController started Post-Mission Survey 2");
                    SceneManager.LoadScene(GeneralSurvey);
                    break;
                default:
                    Debug.Log("ScenFlowController started Final Scene");
                    SceneManager.LoadScene(FinalScene);
                    break;
            }
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