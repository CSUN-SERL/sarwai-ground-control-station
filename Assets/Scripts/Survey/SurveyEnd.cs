using System;
using UnityEngine;

namespace Survey
{
    public class SurveyEnd : MonoBehaviour
    {
        private bool _adaptive;
        private int _mission;

        private void Start()
        {
            _adaptive = false;
            _mission = 2;
            //example
            //EventManager.OnEnd();
        }

        private void OnEnable()
        {
            EventManager.End += OnEnd;
        }

        private void OnDisable()
        {
            EventManager.End -= OnEnd;
        }

        public void NonAdaptive()
        {
            Debug.Log(_mission);

            switch (_mission) //
            {
                case 1:
                    EventManager.OnLoad(1);
                    ++_mission;
                    break;
                case 2:
                    EventManager.OnLoad(2);
                    ++_mission;
                    break;
                case 3:
                    EventManager.OnLoad(3);
                    ++_mission;
                    break;

                case 4:
                    goto case 12;
                case 5:
                    goto case 13;

                case 6:
                    goto case 12;
                case 7:
                    goto case 13;

                case 8:
                    goto case 12;
                case 9:
                    goto case 13;

                case 10:
                    goto case 12;
                case 11:
                    goto case 13;

                case 12:
                    EventManager.OnLoad(4);
                    ++_mission;
                    break;

                case 13:
                    EventManager.OnLoad(5);
                    ++_mission;
                    break;

                case 14:
                    EventManager.OnLoad(20);
                    ++_mission;
                    break;
                case 15:
                    EventManager.OnLoad(21);
                    ++_mission;
                    break;
                case 16:
                    _mission = 1;
                    goto case 1;
            }
        }

        public void Adaptive()
        {
            Debug.Log(_mission);

            switch (_mission) //
            {
                case 1:
                    EventManager.OnLoad(1);
                    ++_mission;
                    break;
                case 2:
                    EventManager.OnLoad(2);
                    ++_mission;
                    break;
                case 3:
                    EventManager.OnLoad(3);
                    ++_mission;
                    break;

                case 4:
                    goto case 16;
                case 5:
                    goto case 17;
                case 6:
                    EventManager.OnLoad(6);
                    ++_mission;
                    break;


                case 7:
                    goto case 16;
                case 8:
                    goto case 17;
                case 9:
                    EventManager.OnLoad(7);
                    ++_mission;
                    break;

                case 10:
                    goto case 16;
                case 11:
                    goto case 17;
                case 12:
                    EventManager.OnLoad(8);
                    ++_mission;
                    break;

                case 13:
                    goto case 16;
                case 14:
                    goto case 17;
                case 15:
                    EventManager.OnLoad(9);
                    ++_mission;
                    break;

                case 16:
                    EventManager.OnLoad(10);
                    ++_mission;
                    break;

                case 17:
                    EventManager.OnLoad(5);
                    ++_mission;
                    break;
                case 18:
                    EventManager.OnLoad(3);
                    ++_mission;
                    break;
                case 19:
                    EventManager.OnLoad(20);
                    ++_mission;
                    break;
                case 20:
                    EventManager.OnLoad(21);
                    ++_mission;
                    break;
                case 21:
                    _mission = 1;
                    goto case 1;
            }
        }

        private void OnEnd(object sender, EventArgs e)
        {
            if (_adaptive)
                Adaptive();
            else
                NonAdaptive();
            Debug.Log("Survey Finished");
        }
    }
}