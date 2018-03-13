using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using Mission;
using UnityEngine;
using UnityEngine.UI;

namespace Question
{
    public class QuestionButtonManager : MonoBehaviour
    {
        private Button _blackButton;
        private Button _greenButton;

        private Button _micButton;
        private Button _noButton;

        private Query _query;
        private Button _redButton;
        private Button _yellowButton;
        private Button _yesButton;
        public GameObject QueryGameObject;

        // Use this for initialization
        private void Awake()
        {
            Transform tempGameObject;
            if ((tempGameObject = gameObject.transform.Find("MicButton")) !=
                null)
            {
                _micButton = tempGameObject.GetComponent<Button>();
                _micButton.onClick.AddListener(MicButtonOnClick);
            }

            if ((tempGameObject = gameObject.transform.Find("GreenButton")) !=
                null)
            {
                _greenButton = tempGameObject.GetComponent<Button>();
                _greenButton.onClick.AddListener(GreenButtonOnClick);
            }

            if ((tempGameObject = gameObject.transform.Find("YellowButton")) !=
                null)
            {
                _yellowButton = tempGameObject.GetComponent<Button>();
                _yellowButton.onClick.AddListener(YellowButtonOnClick);
            }

            if ((tempGameObject = gameObject.transform.Find("RedButton")) !=
                null)
            {
                _redButton = tempGameObject.GetComponent<Button>();
                _redButton.onClick.AddListener(RedButtonOnClick);
            }

            if ((tempGameObject = gameObject.transform.Find("BlackButton")) !=
                null)
            {
                _blackButton = tempGameObject.GetComponent<Button>();
                _blackButton.onClick.AddListener(BlackButtonOnClick);
            }

            if ((tempGameObject = gameObject.transform.Find("YesButton")) !=
                null)
            {
                _yesButton = tempGameObject.GetComponent<Button>();
                _yesButton.onClick.AddListener(YesButtonOnClick);
            }

            if ((tempGameObject = gameObject.transform.Find("NoButton")) !=
                null)
            {
                _noButton = tempGameObject.GetComponent<Button>();
                _noButton.onClick.AddListener(NoButtonOnClick);
            }
        }

        private void OnEnable()
        {
            DisplayEventManager.TagQuestion += DisplayTagQuestion;
            DisplayEventManager.BoolQuestion += DisplayBoolQuestion;
            DisplayEventManager.ClearDisplay += Clear;
        }

        private void OnDisable()
        {
            DisplayEventManager.TagQuestion -= DisplayTagQuestion;
            DisplayEventManager.BoolQuestion -= DisplayBoolQuestion;
            DisplayEventManager.ClearDisplay -= Clear;
        }

        private void DisplayBoolQuestion(object sender, QueryEventArgs e)
        {
            _query = e.Query;
            _yesButton.gameObject.SetActive(true);
            _noButton.gameObject.SetActive(true);
        }

        private void DisplayTagQuestion(object sender, QueryEventArgs e)
        {
            _query = e.Query;
            _greenButton.gameObject.SetActive(true);
            _yellowButton.gameObject.SetActive(true);
            _redButton.gameObject.SetActive(true);
            _blackButton.gameObject.SetActive(true);
        }

        private void Clear(object sender, EventArgs e)
        {
            _greenButton.gameObject.SetActive(false);
            _yellowButton.gameObject.SetActive(false);
            _redButton.gameObject.SetActive(false);
            _blackButton.gameObject.SetActive(false);
            _yesButton.gameObject.SetActive(false);
            _noButton.gameObject.SetActive(false);
        }


        private void GreenButtonOnClick()
        {
            ButtonOnClick(0);
        }

        private void YellowButtonOnClick()
        {
            ButtonOnClick(1);
        }

        private void RedButtonOnClick()
        {
            ButtonOnClick(2);
        }

        private void BlackButtonOnClick()
        {
            ButtonOnClick(3);
        }

        private void YesButtonOnClick()
        {
            ButtonOnClick(1);
        }

        private void NoButtonOnClick()
        {
            ButtonOnClick(0);
        }


        private void ButtonOnClick(int answer)
        {
            _query.Response = answer;
            MissionEventManager.OnQueryAnswered(_query);
            DisplayEventManager.OnClearDisplay();
        }

        private static void MicButtonOnClick()
        {
            Debug.Log("Mic Active");
        }
    }
}