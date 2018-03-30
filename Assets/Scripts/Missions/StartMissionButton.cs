using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
    public class StartMissionButton : MonoBehaviour
    {
        public int WaitTime = 60;
        private Button _button;

        void Awake()
        {
            StartCoroutine(StartWait(WaitTime));
        }

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(MissionLifeCycleController
                .StartMission);
        }

        private IEnumerator StartWait(int waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _button.interactable = true;
        }

    }
}