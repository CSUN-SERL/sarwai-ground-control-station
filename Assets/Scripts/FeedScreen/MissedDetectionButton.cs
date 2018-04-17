using Networking;
using UnityEngine;
using UnityEngine.UI;

public class MissedDetectionButton : MonoBehaviour
{
    // setup missed detection buttons
    public Button MissedDetection1;
    public Button MissedDetection2;
    public Button MissedDetection3;
    public Button MissedDetection4;

    void OnEnable()
    {

        Button button1 = MissedDetection1.GetComponent<Button>();
        Button button2 = MissedDetection2.GetComponent<Button>();
        Button button3 = MissedDetection3.GetComponent<Button>();
        Button button4 = MissedDetection4.GetComponent<Button>();

        // add listeners to buttons
        button1.onClick.AddListener(OnButtonClick1);
        button2.onClick.AddListener(OnButtonClick2);
        button3.onClick.AddListener(OnButtonClick3);
        button4.onClick.AddListener(OnButtonClick4);

    }

    void OnButtonClick1()
    {
        RosbridgeSocket.Emit(ServerURL.MISSED_DETECTION_TOPIC, "1");
    }
    void OnButtonClick2()
    {
        RosbridgeSocket.Emit(ServerURL.MISSED_DETECTION_TOPIC, "2");
    }
    void OnButtonClick3()
    {
        RosbridgeSocket.Emit(ServerURL.MISSED_DETECTION_TOPIC, "3");
    }
    void OnButtonClick4()
    {
        RosbridgeSocket.Emit(ServerURL.MISSED_DETECTION_TOPIC, "4");
    }
}