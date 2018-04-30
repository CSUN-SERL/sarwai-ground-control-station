using Networking;
using UnityEngine;
using UnityEngine.UI;

public class MissedDetectionButton : MonoBehaviour
{
    // setup missed detection buttons
    public Button MissedDetection;

    public int RobotId;

    void OnEnable()
    {

        Button button1 = GetComponent<Button>();

        // add listeners to buttons
        button1.onClick.AddListener(OnButtonClick);

    }

    void OnButtonClick()
    {
        RosbridgeSocket.Emit(ServerURL.MISSED_DETECTION_TOPIC, RobotId.ToString());
    }
}