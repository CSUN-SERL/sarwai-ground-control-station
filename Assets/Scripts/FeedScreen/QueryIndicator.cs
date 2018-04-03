using System;
using System.Collections;
using System.Collections.Generic;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using JetBrains.Annotations;
using Mission;
using Networking;
using UnityEngine;
using UnityEngine.UI;


public class QueryIndicator : MonoBehaviour
{

    // this is the UI.Text or other UI element you want to toggle
    public MaskableGraphic Image1, Image2, Image3, Image4;
    public MaskableGraphic Text1, Text2, Text3, Text4;

    private int _robotID;


    private int _numBlinks = 6;
    public float Interval;
    public float StartDelay;
    public bool CurrentState = true;
    public bool DefaultState = true;
    // _isBlinking prevents us from starting the ToggleState again if its already going.
    private bool _isBlinking = false;
    // use this thingamabob to start and stop the query indicator.
    private static bool _noticeMeSenpai = false;

    void Start()
    {
        // StartBlink();
    }


    private void OnEnable()
    {
        SocketEventManager.QueryGenerated += OnQueryGenerated;
    }

    private void OnDisable()
    {
        SocketEventManager.QueryGenerated -= OnQueryGenerated;
    }

    public void OnQueryGenerated(string data, EventArgs e)
    {
        if (!_isBlinking)
        {
            _robotID = int.Parse(data);
        }

        Debug.Log("Made it to QueryIndicator.cs " + _robotID);
        _noticeMeSenpai = true;
    }

    void Update()
    {
        // want to do StartBlink() once per generated query
        if (_noticeMeSenpai == true)
        {
            StartBlink();
        }
        _noticeMeSenpai = false;
    }

    public void StartBlink()
    {
        // do not invoke the blink twice - needed if you need to start the blink from an external object
        if (_isBlinking)
            return;

        if (Image1 != null && Text1 != null)
        {
            _isBlinking = true;
            InvokeRepeating("ToggleState", StartDelay, Interval);
        }
    }

    // Kind of a silly implementation, but if it works, I'm happy.
    public void ToggleState()
    {
        if (_robotID == 1)
        {
            Image1.enabled = !Image1.enabled;
            Text1.enabled = !Text1.enabled;
            // use numBlinks to ensure fixed number of blinks per generated query.
            _numBlinks--;
            if (_numBlinks == 0)
            {
                _isBlinking = false;
                _numBlinks = 6;
                Image1.enabled = false;
                Text1.enabled = false;
                CancelInvoke("ToggleState");
            }
        }
        else if (_robotID == 2)
        {
            Image2.enabled = !Image2.enabled;
            Text2.enabled = !Text2.enabled;
            // use numBlinks to ensure fixed number of blinks per generated query.
            _numBlinks--;
            if (_numBlinks == 0)
            {
                _isBlinking = false;
                _numBlinks = 6;
                Image2.enabled = false;
                Text2.enabled = false;
                CancelInvoke("ToggleState");
            }
        }
        else if (_robotID == 3)
        {
            Image3.enabled = !Image3.enabled;
            Text3.enabled = !Text3.enabled;
            // use numBlinks to ensure fixed number of blinks per generated query.
            _numBlinks--;
            if (_numBlinks == 0)
            {
                _isBlinking = false;
                _numBlinks = 6;
                Image3.enabled = false;
                Text3.enabled = false;
                CancelInvoke("ToggleState");
            }
        }
        else if (_robotID == 4)
        {
            Image4.enabled = !Image4.enabled;
            Text4.enabled = !Text4.enabled;
            // use numBlinks to ensure fixed number of blinks per generated query.
            _numBlinks--;
            if (_numBlinks == 0)
            {
                _isBlinking = false;
                _numBlinks = 6;
                Image4.enabled = false;
                Text4.enabled = false;
                CancelInvoke("ToggleState");
            }
        }
        else { Debug.Log("Invalid robotID"); }
    }
}
