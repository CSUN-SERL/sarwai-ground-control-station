using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
    public class StartMissionButton : MonoBehaviour
    {
        public int WaitTime = 1;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(MissionLifeCycleController
                .StartMission);
            StartCoroutine(StartWait(WaitTime));
        }

        private IEnumerator StartWait(int waitTime)
        {
            Debug.Log(waitTime);
            yield return new WaitForSecondsRealtime(waitTime);
            _button.interactable = true;
        }

    }
}