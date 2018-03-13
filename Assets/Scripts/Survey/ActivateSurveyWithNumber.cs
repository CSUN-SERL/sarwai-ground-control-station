using Participant;
using UnityEngine;

namespace Survey
{
    public static class ActivateSurveyWithNumber
    {
        public static void Adaptive()
        {
            Debug.Log("Adaptive in ActiveSurveyWithNumber");
            var surveyNumber = ParticipantBehavior.Participant.CurrentSurvey;

            const int DEBRIEF_SURVEY = 69;
            Debug.Log("switch, Adaptive in ActiveSurveyWithNumber");
            switch (surveyNumber)
            {
                case 1:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 1");
                    EventManager.OnLoad(1);
                    break;
                case 2:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 2");
                    EventManager.OnLoad(2);
                    break;
                case 3:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 3");
                    EventManager.OnLoad(3);
                    break;

                case 4:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 69");
                    EventManager.OnLoad(DEBRIEF_SURVEY);
                    break;
                case 5:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 5");
                    EventManager.OnLoad(5);
                    break;
                case 6:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 6");
                    EventManager.OnLoad(6);
                    break;

                case 7:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 7");
                    EventManager.OnLoad(DEBRIEF_SURVEY);
                    break;
                case 8:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 8");
                    EventManager.OnLoad(5);
                    break;
                case 9:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 9");
                    EventManager.OnLoad(6);
                    break;

                case 10:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 10");
                    EventManager.OnLoad(DEBRIEF_SURVEY);
                    break;
                case 11:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 11");
                    EventManager.OnLoad(5);
                    break;
                case 12:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 12");
                    EventManager.OnLoad(6);
                    break;

                case 13:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 13");
                    EventManager.OnLoad(DEBRIEF_SURVEY);
                    break;
                case 14:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 14");
                    EventManager.OnLoad(5);
                    break;
                case 15:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 15");
                    EventManager.OnLoad(6);
                    break;

                case 16:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 16");
                    EventManager.OnLoad(5);
                    break;

                case 17:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 17");
                    EventManager.OnLoad(6);
                    break;
                case 18:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 18");
                    EventManager.OnLoad(20);
                    break;
                case 19:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 19");
                    EventManager.OnLoad(21);
                    break;
                default:
                    Debug.Log("ActivateSurveyWithNumbers to load default, " + surveyNumber);
                    EventManager.OnEnd();
                    break;
            }
        }

        public static void NonAdaptive()
        {
            Debug.Log("NonAdaptive in ActiveSurveyWithNumber");
            var surveyNumber = ParticipantBehavior.Participant.CurrentSurvey;


            Debug.Log(
                "switch, NonAdaptive in ActiveSurveyWithNumber, surveyNumber=" +
                surveyNumber);
            switch (surveyNumber)
            {
                case 1:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 1");
                    EventManager.OnLoad(1);
                    break;
                case 2:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 2");
                    EventManager.OnLoad(2);
                    break;
                case 3:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 3");
                    EventManager.OnLoad(3);
                    break;

                case 4:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 4");
                    EventManager.OnLoad(4);
                    break;
                case 5:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 5");
                    EventManager.OnLoad(6);
                    break;

                case 6:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 6");
                    EventManager.OnLoad(4);
                    break;
                case 7:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 7");
                    EventManager.OnLoad(6);
                    break;
                case 8:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 8");
                    EventManager.OnLoad(4);
                    break;

                case 9:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 9");
                    EventManager.OnLoad(6);
                    break;
                case 10:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 10");
                    EventManager.OnLoad(4);
                    break;
                case 11:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 11");
                    EventManager.OnLoad(6);
                    break;

                case 12:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 12");
                    EventManager.OnLoad(4);
                    break;
                case 13:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 13");
                    EventManager.OnLoad(6);
                    break;
                case 14:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 14");
                    EventManager.OnLoad(20);
                    break;

                case 15:
                    Debug.Log("ActivateSurveyWithNumbers calls OnLoad 15");
                    EventManager.OnLoad(21);
                    break;
                default:
                    Debug.Log("ActivateSurveyWithNumbers to default");
                    EventManager.OnEnd();
                    break;
            }
        }

        /*
        private void OnDisable()
        {
            EventManager.End -= this.OnEnd;
        }
        private void OnEnable()
        {
            EventManager.End += this.OnEnd;
        }
        private void OnEnd(object sender, PhysiologicalDataEventArgs e)
        {
            var adaptive = Participant.Instance.Data.Adaptive;

            if (adaptive) this.Adaptive();
            else this.NonAdaptive();

            Debug.Log("Survey Finished");
        }
         */
        public static void CallListener()
        {
            bool adaptive = false;
            if (ParticipantBehavior.Participant != null)
                adaptive = ParticipantBehavior.Participant.Data.Adaptive;
            else //Done so that unit test will work
            {
                adaptive = true;
            }
            if (adaptive) Adaptive();
            else NonAdaptive();

            // example
            // EventManager.OnEnd();
        }
    }
}