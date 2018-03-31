using System;
using Mission;
using Mission.Lifecycle;
using UnityEngine;
using UnityEngine.UI;

namespace Menu_Navigation.Mission
{
    public class StartMenuScript : MonoBehaviour
    {
        private GameObject _popup;
        public Text DescriptionText;
        public Text MissionNumberText;

        private void Start()
        {
        }

        private void OnEnable()
        {
            EventManager.MetaDataLoaded += OnDisplayBrief;
            EventManager.Started += OnStarted;
        }

        private void OnDisable()
        {
            EventManager.MetaDataLoaded -= OnDisplayBrief;
            EventManager.Started -= OnStarted;
        }

        private void OnStarted(object sender, EventArgs e)
        {
            _popup.SetActive(false);
        }

        private void OnDisplayBrief(object sender, MissionEventArgs e)
        {
            //TODO Resize Start Popup to changing screen size.
            //_popup.transform.position = new Vector3(x: Screen.width / 2.0F, y: Screen.height / 2.0F);
            //_popup.transform.localScale = new Vector3(2.0F, 2.0F);

            _popup = gameObject.transform.GetChild(0).gameObject;
            _popup.SetActive(true);

            var minutes = Mathf.Floor(e.MissionLength / 60).ToString("00");
            var seconds = (e.MissionLength % 60).ToString("00");

            MissionNumberText.text =
                string.Format("Mission {0}", e.MissionNumber);
            DescriptionText.text = e.MissionBrief + "\nMission Length: " +
                                   minutes + ":" + seconds;
            _popup.SetActive(true);
        }
    }
}