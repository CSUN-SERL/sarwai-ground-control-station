using FeedScreen.Experiment.Missions.Broadcasts.Events;
using Participant;
using UnityEngine;

namespace Survey
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
                }
            };

            EventManager.OnLoad(SurveyNumber);
        }

        private void OnEnable()
        {
            EventManager.Load += OnLoad;
        }

        private void OnLoad(object sender, IntEventArgs e)
        {
            Debug.Log(e.intField);
        }

        private void OnDisable()
        {
            EventManager.Load -= OnLoad;
        }
    }
}