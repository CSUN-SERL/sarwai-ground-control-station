using System;
using Mission;
using Networking;
using UnityEngine;
using UnityEngine.UI;

public class RobotDisplay : MonoBehaviour
{

    public int RobotId;
    private static int _numDisplays;

    public Text DescriptionText;

    public Text QACounter;
    private int _numQAuto;

    public Text QSCounter;
    private int _numQStop;

    public Button MissedDetectionButton;

    public IpCameraStream LiveFeed;


    public void OnEnable() {

        SocketEventManager.AutonomousQuery += OnAutonomousQuery;
        SocketEventManager.QueryRecieved += OnQueryReceived;
        Mission.Lifecycle.EventManager.Started += OnMissionStarted;
        Mission.Lifecycle.EventManager.Stopped += OnMissionStopped;

        DescriptionText.text = string.Format("Robot {0} Live Feed", RobotId);
        LiveFeed.sourceUrl = ServerURL.GetRobotLiveStream(RobotId);
    }

    public void OnDisable() {
        SocketEventManager.AutonomousQuery -= OnAutonomousQuery;
        SocketEventManager.QueryRecieved -= OnQueryReceived;
    }

    private void OnMissionStarted(object sender, EventArgs e)
    {
        LiveFeed.PlayLiveFeed();
    }

    private void OnMissionStopped(object sender, EventArgs e)
    {
        LiveFeed.StopLiveFeed();   
    }

    void Awake()
    {
        RobotId = ++_numDisplays;
        MissedDetectionButton.onClick.AddListener(OnMissedDetectionButtonClicked);
    }

    void OnMissedDetectionButtonClicked() {
        RosbridgeSocket.Emit(ServerURL.MISSED_DETECTION_TOPIC, RobotId.ToString());
    }

    private void OnQueryReceived(object sender, QueryEventArgs e)
    {
        if (e.Query.RobotId == RobotId)
        {
            _numQStop++;
            QSCounter.text = string.Format("QS: {0}", _numQStop);
        }
    }

    private void OnAutonomousQuery(string obj, EventArgs args)
    {
        _numQAuto++;
        QSCounter.text = string.Format("QS: {0}", _numQAuto);
    }
}
