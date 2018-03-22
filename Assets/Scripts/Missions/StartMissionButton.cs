using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
    public class StartMissionButton : MonoBehaviour
    {
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(MissionLifeCycleController
                .StartMission);
        }
    }
}