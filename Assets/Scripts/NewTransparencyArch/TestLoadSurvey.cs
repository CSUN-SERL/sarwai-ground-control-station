using FeedScreen.Experiment.Missions.Broadcasts.Events;
using Participant;
using UnityEngine;

namespace NewTransparencyArch
{
    public class TestLoadSurvey : MonoBehaviour
    {

        public int SurveyNumber;

        private void Start()
        {
            ParticipantBehavior.Participant = new Participant.Participant
            {
                Data = new ParticipantData
                {
                    Group = 3
                },
                //CurrentSurvey= SurveyNumber
            };

            EventManager.OnFetchSurveyFromWeb(SurveyNumber);
        }

        private void OnEnable()
        {
            EventManager.FetchSurveyFromWeb += OnLoad;
        }

        private void OnLoad(object sender, IntEventArgs e)
        {
            Debug.Log(e.intField);
        }

        private void OnDisable()
        {
            EventManager.FetchSurveyFromWeb -= OnLoad;
        }
    }
}