using System;
using System.Security.Policy;
using LiveFeedScreen.ROSBridgeLib;
using LiveFeedScreen.ROSBridgeLib.std_msgs.std_msgs;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using WebSocketSharp;

public class MissedDetectionButton : MonoBehaviour
{
    //private WebSocket _k;
    // setup missed detection buttons
    public Button MissedDetection1;
    public Button MissedDetection2;
    public Button MissedDetection3;
    public Button MissedDetection4;

    // create a rossocket connection
    private ROSBridgeWebSocketConnection _rosA = null;

    // setup constants to hold ips, port and msg
    //private const string Station1 = "ws:ubuntu@192.168.1.161";
    private const string Station1 = "ws://192.168.1.161";
    private const string Station4 = "ws:ubuntu@192.168.1.43";
    private const string Topic = "/coffee";
    private const int Port = 9090;
    private StringMsg _msg; 


    void Start()
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

        _msg = new StringMsg("Missed Detection. (Couldn't get it up)");

        
        //_rosA.AddPublisher(typeof(Publisher));
        // initialize connection
        _rosA = new ROSBridgeWebSocketConnection(Station4, Port);
        _rosA.Connect();
     
        _rosA.Publish(Topic, _msg);
    }

    void OnButtonClick1()
    {
        _msg = new StringMsg("Missed Detection. (Couldn't get it up)");
        _rosA.Publish(Topic, _msg);
        Debug.Log("Missed Detection. Bot 1");
        _rosA.Render();
    }
    void OnButtonClick2()
    {
        _rosA.Publish(Topic, _msg);
        Debug.Log("Missed Detection. Bot 2");
        _rosA.Render();
    }
    void OnButtonClick3()
    {
        _rosA.Publish(Topic, _msg);
        Debug.Log("Missed Detection. Bot 3");
        _rosA.Render();
    }
    void OnButtonClick4()
    {
        _rosA.Publish(Topic, _msg);
        Debug.Log("Missed Detection. Bot 4");
        _rosA.Render();
    }

    private void OnApplicationQuit()
    {
        if (_rosA != null)
        {
            _rosA.Disconnect();
        }
    }
}
