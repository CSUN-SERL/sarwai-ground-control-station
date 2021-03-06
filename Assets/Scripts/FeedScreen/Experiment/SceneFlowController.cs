﻿using System.Collections.Generic;
using Participant;
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
        public const string GeneralSurvey = "SurveyScene";
        public const string TransparencyBrief = "TransparentBrief";
        public const string MissionScene = "MissionScene";
        public const string FinalScene = "FinalScene";
        public const string Error = "Error";

        private int _numMissions = 5;
        
        private readonly List<SceneNode> Timeline = new List<SceneNode>();
        private int CurrentScene;

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

        private void OnEnable()
        {
            EventManager.NewParticipantMade += OnNewParticipantMade;
        }

        private void OnDisable() {
            EventManager.NewParticipantMade -= OnNewParticipantMade;
        }

        private void OnNewParticipantMade(object sender, NewParticipantEventArgs e)
        {
            // Initialize Participant Timeline
            Timeline.Add(
                new SceneNode {
                    SceneName = ProctorSetup
                });

            // Welcome Screen
            Timeline.Add(
                new SceneNode {
                    SceneName = Welcome
                });

            // Initial Surveys
            Timeline.Add(
                new SurveyScene {
                    SceneName = GeneralSurvey,
                    SurveyName = "DemographicSurvey",
                    SurveyId = 1
                });
            Timeline.Add(
                new SurveyScene {
                    SceneName = GeneralSurvey,
                    SurveyName = "Ipip",
                    SurveyId = 2
                });
            Timeline.Add(
                new SurveyScene {
                    SceneName = GeneralSurvey,
                    SurveyName = "InitialTrust",
                    SurveyId = 3
                });


            // Mission Loop
            for (int i = 1; i < _numMissions + 1; i++) {

                // Add transparency briefs to transparent participant and
                if (ParticipantBehavior.Participant.Data.Transparent && i != 1) {
                    Timeline.Add(
                        new SceneNode {
                            SceneName = TransparencyBrief
                        });
                }

                // Add Mission
                Timeline.Add(
                    new MissionScene {
                        SceneName = MissionScene,
                        MissionId = i
                    });


                // Add Adaptive Trust or Non Adaptive Trust.
                if (!ParticipantBehavior.Participant.Data.Adaptive)
                {
                    Timeline.Add(
                        new SurveyScene {
                            SceneName = GeneralSurvey,
                            SurveyName = "NonAdaptiveTrust",
                            SurveyId = 4
                        });
                }
                else
                {
                    Timeline.Add(
                        new SurveyScene {
                            SceneName = GeneralSurvey,
                            SurveyName = "AdaptiveTrust",
                            SurveyId = 5
                        });
                }

                Timeline.Add(
                    new SurveyScene {
                        SceneName = GeneralSurvey,
                        SurveyName = "Tlx",
                        SurveyId = 6
                    });
            }

            Timeline.Add(
                new SurveyScene {
                    SceneName = GeneralSurvey,
                    SurveyName = "EndTlx",
                    SurveyId = 20
                });

            Timeline.Add(
                new SurveyScene {
                    SceneName = GeneralSurvey,
                    SurveyName = "EndOfExperiment",
                    SurveyId = 21
                });

            Timeline.Add(
                    new SceneNode {
                        SceneName = FinalScene
                    });

            CurrentScene = 0;
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
            Instance.CurrentScene++;
            Instance.Timeline[Instance.CurrentScene].LoadScene();
        }


        /// <summary>
        /// The error scene is a catch-all for all critical system errors that are non recoverable.
        /// </summary>
        public static void LoadErrorScene()
        {
            SceneManager.LoadScene(Error);
        }

        /// <summary>
        /// This is a wrapper for gameobjects in unity to go to the next mission.
        /// </summary>
        public void LoadNextSceneWrapper()
        {
            LoadNextScene();
        }
    }

    public class SceneNode
    {
        public string SceneName;

        public virtual void LoadScene()
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    public class MissionScene : SceneNode
    {
        public int MissionId { get; set; }

        public override void LoadScene()
        {
            base.LoadScene();
            ParticipantBehavior.Participant.CurrentMission = MissionId;
        }
    }

    public class SurveyScene : SceneNode
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }

        public override void LoadScene() {
            base.LoadScene();
            ParticipantBehavior.Participant.CurrentSurvey = SurveyId;
        }
    }
}
