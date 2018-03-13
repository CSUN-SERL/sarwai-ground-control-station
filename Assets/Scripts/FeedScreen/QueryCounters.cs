using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueryCounters : MonoBehaviour
{

    public Text QA1;
    public Text QA2;
    public Text QA3;
    public Text QA4;

    public static int Counter;

    // event used to increment count when GCSSocket.cs receives a notification that a 
    // query was handled autonomously.
    public static event EventHandler<EventArgs> IncrementCount;

    public void Start()
    {
        QA1.text = "QA: 0";
    }

    public void Update()
    {
        QA1.text = "QA: " + Counter;
    }


    public static void OnIncrementCount()
    {
        Counter++;
        Debug.Log("We got that shit boyeee! ");
    }
}
