using System;
using System.Collections.Generic;
using Mission.Lifecycle;
using UnityEngine;

namespace Participant
{
    public class Participant
    {
        private static Participant _instance;

        private const int Transparency= -1;
        private const int MissionScene = 0;
        private const int DemographiSurvey = 1;
        private const int Ipip = 2;
        private const int InitialTrust = 3;
        private const int NonAdaptiveTrust = 4;
        private const int AdaptiveTrust = 5;
        private const int Tlx = 6;
        private const int PerformanceMetrics = 7;
        private const int EndofExperiment = 21;
        private const int EndTlx = 20;
        private const int EndScene = -2;


        public const string ProctorSetup = "ProctorSetup";
        public const string Welcome = "Welcome";
        public const string SurveyScene = "SurveyScene";
        public const string TransparencyBrief = "TransparentBrief";
        public const string MissionScreen = "MissionScene";
        public const string PerformanceMetricsScene = "PerformanceDebrief";
        public const string FinalScene = "FinalScene";
        public const string Error = "Error";

        private readonly List<List<int>> Timelines = new List<List<int>>
        {
            new List<int> // Group 1 (Adaptive Transparent) tested on 3/20
            {
                DemographiSurvey,
                Ipip,
                InitialTrust,
                              MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                Transparency, MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                Transparency, MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                Transparency, MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                Transparency, MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                EndTlx,
                EndofExperiment,
                EndScene
            },
            new List<int> // Group 2 (Adaptive NonTransparent) tested on 3/20
            {
                DemographiSurvey,
                Ipip,
                InitialTrust,
                MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                MissionScene, PerformanceMetrics, AdaptiveTrust, Tlx,
                EndTlx,
                EndofExperiment,
                EndScene
            },
            new List<int> // Group 3 (NonAdaptive NonTransparent)
            {
                DemographiSurvey,
                Ipip,
                InitialTrust,
                MissionScene, PerformanceMetrics, NonAdaptiveTrust, Tlx,
                MissionScene, PerformanceMetrics, NonAdaptiveTrust, Tlx,
                MissionScene, PerformanceMetrics, NonAdaptiveTrust, Tlx,
                MissionScene, PerformanceMetrics, NonAdaptiveTrust, Tlx,
                MissionScene, PerformanceMetrics, NonAdaptiveTrust, Tlx,
                EndTlx,
                EndofExperiment,
                EndScene
            }
        };
        

        public Participant()
        {
            _instance = this;
            Mission.Lifecycle.EventManager.Completed += OnCompleted;
            NewSurveyArch.EventManager.PushedSurvey += OnSurveyEnd;
            TransparencyIrisToOperator.EventManager.End += OnTransparencyBriefEnd;

        }

        public ParticipantData Data { get; set; }

        public int CurrentMission { get; set; }
        public int CurrentTimeline { get; set; }
        public int CurrentSurvey { get {
                Debug.Log("timeline = " + Instance.CurrentTimeline); return Timelines[Data.Group - 1][Instance.CurrentTimeline]; } }
        public int CurrentScene { get { return Timelines[Data.Group-1][Instance.CurrentTimeline]; } }
        

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
                        CurrentTimeline = 0
                    };
                return _instance;
            }
        }

        ~Participant()
        {
            Mission.Lifecycle.EventManager.Completed -= OnCompleted;
            NewSurveyArch.EventManager.PushedSurvey -= OnSurveyEnd;
            TransparencyIrisToOperator.EventManager.End -= OnTransparencyBriefEnd;
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            Instance.CurrentTimeline += 1;
            Instance.CurrentMission += 1;
            Debug.Log(string.Format(
                "Participant Current Timeline incremented from {0} to {1}",
                Instance.CurrentTimeline - 1, Instance.CurrentTimeline));
        }

        private void OnSurveyEnd(object sender, EventArgs e)
        {
            Instance.CurrentTimeline += 1;
            Debug.Log(string.Format(
                "Participant Current Timeline incremented from {0} to {1}",
                Instance.CurrentTimeline - 1, Instance.CurrentTimeline));
        }

        private void OnTransparencyBriefEnd(object sender, EventArgs e)
        {
            Instance.CurrentTimeline += 1;
            Debug.Log(string.Format(
                "Participant Current Timeline incremented from {0} to {1}",
                Instance.CurrentTimeline - 1, Instance.CurrentTimeline));
        }


    }
}