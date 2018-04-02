using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveFeedScreen.ROSBridgeLib;
using LiveFeedScreen.ROSBridgeLib.sensor_msgs.sensor_msgs;

public class DisplayCompressedImage : MonoBehaviour {
    private ROSBridgeWebSocketConnection _ros = null;
	// Use this for initialization
	void Start () {
        _ros = new ROSBridgeWebSocketConnection ("ws:ubuntu@192.168.1.43", 9090);
        _ros.AddSubscriber(typeof(CompressedImage));
	    _ros.AddSubscriber(typeof(CompressedImage2));
	    _ros.AddSubscriber(typeof(CompressedImage3));
	    _ros.AddSubscriber(typeof(CompressedImage4));

        _ros.Connect ();    
	}
    void OnApplicationQuit()
    {
        if (_ros != null)
            _ros.Disconnect();
    }
	// Update is called once per frame
	void Update () {
        

        _ros.Render();
	}
}
