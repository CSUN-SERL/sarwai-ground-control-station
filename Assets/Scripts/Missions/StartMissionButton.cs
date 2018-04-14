using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
    public class StartMissionButton : MonoBehaviour
    {

        private Button _button;
        private bool _missionReady;

        void OnEnable()
        {
            Lifecycle.EventManager.Ready += OnReady;
        }

        void OnDisable() {
            Lifecycle.EventManager.Ready -= OnReady;
        }

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(MissionLifeCycleController
                .StartMission);
        }

        void Update()
        {
            _button.interactable = MissionLifeCycleController.Instance.Ready;
        }

        private void OnReady(object sender, EventArgs e)
        {
            _missionReady = true;
        }
    }
}