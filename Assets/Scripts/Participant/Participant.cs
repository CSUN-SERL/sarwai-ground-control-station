using System;
using Mission.Lifecycle;
using UnityEngine;

namespace Participant
{
    public class Participant
    {
        private static Participant _instance;

        public Participant()
        {
            _instance = this;
            Mission.Lifecycle.EventManager.Completed += OnCompleted;
            Survey.EventManager.End += OnSurveyEnd;
        }

        public ParticipantData Data { get; set; }

        public int CurrentMission { get; set; }
        public int CurrentSurvey { get; set; }

        public bool isDone
        {
            get { return CurrentMission > 5; }
        }

        public static Participant Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Participant
                    {
                        CurrentMission = 1,
                        CurrentSurvey = 1
                    };
                return _instance;
            }
        }

        ~Participant()
        {
            Mission.Lifecycle.EventManager.Completed -= OnCompleted;
            Survey.EventManager.End -= OnSurveyEnd;
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            Instance.CurrentMission += 1;
            Debug.Log(string.Format(
                "Participant Current Mission incremented from {0} to {1}",
                Instance.CurrentMission - 1, Instance.CurrentMission));
        }

        private void OnSurveyEnd(object sender, EventArgs e)
        {
            Instance.CurrentSurvey += 1;
            Debug.Log(string.Format(
                "Participant Current Survey incremented from {0} to {1}",
                Instance.CurrentSurvey - 1, Instance.CurrentSurvey));
        }
    }
}