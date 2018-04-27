using System;
using UnityEngine;

namespace Participant
{
    public class Participant
    {
        private static Participant _instance;

        public ParticipantData Data { get; set; }

        public int CurrentMission { get; set; }

        public int CurrentSurvey { get; set; }

        public static Participant Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Participant();
                return _instance;
            }
        }
       
    }
}