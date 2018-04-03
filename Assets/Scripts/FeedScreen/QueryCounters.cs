using System;
using System.Collections;
using System.Collections.Generic;
using Mission;
using UnityEngine;
using UnityEngine.UI;

public class QueryCounters : MonoBehaviour
{

    private int _robotIDa, _robotIDb;

    public Text QA1, QA2, QA3, QA4;
    public Text QS1, QS2, QS3, QS4;
    public static int QACounter1, QACounter2, QACounter3, QACounter4;
    public static int QSCounter1, QSCounter2, QSCounter3, QSCounter4;

    public static int Counter;

    public void Start()
    {
        QA1.text = "QA: 0";
        QA2.text = "QA: 0";
        QA3.text = "QA: 0";
        QA4.text = "QA: 0";

        QS1.text = "QS: 0";
        QS2.text = "QS: 0";
        QS3.text = "QS: 0";
        QS4.text = "QS: 0";
    }

    public void OnEnable()
    {
        // Reset counters at beginning of each mission.
        QA1.text = "QA: 0";
        QA2.text = "QA: 0";
        QA3.text = "QA: 0";
        QA4.text = "QA: 0";

        QS1.text = "QS: 0";
        QS2.text = "QS: 0";
        QS3.text = "QS: 0";
        QS4.text = "QS: 0";

        SocketEventManager.AutonomousQuery += OnAutonomousQuery;
        // On query received, in this case used to increment Q-Stop.
        // From my understanding the GCS receives queries that are not 
        // autonomously handled.  So this should work fine.
        SocketEventManager.QueryRecieved += OnQueryReceived;
    }

    public void OnDisable()
    {
        SocketEventManager.AutonomousQuery -= OnAutonomousQuery;
        SocketEventManager.QueryRecieved -= OnQueryReceived;
    }

    // event used to increment count when GCSSocket.cs receives a notification that a 
    // query was handled autonomously.
    private void OnAutonomousQuery(string data, EventArgs args)
    {
        _robotIDa = int.Parse(data);

        // Do logic checking increment counters here.
        switch (_robotIDa)
        {
            case 1:
                QACounter1++;
                break;
            case 2:
                QACounter2++;
                break;
            case 3:
                QACounter3++;
                break;
            case 4:
                QACounter4++;
                break;
            default:
                Debug.Log("Invalid Robot ID. ");
                break;
        }
    }

    private void OnQueryReceived(object data, QueryEventArgs e)
    {
        _robotIDb = e.Query.RobotId;

        // Do logic checking increment counters here.
        switch (_robotIDb)
        {
            case 1:
                QSCounter1++;
                break;
            case 2:
                QSCounter2++;
                break;
            case 3:
                QSCounter3++;
                break;
            case 4:
                QSCounter4++;
                break;
            default:
                Debug.Log("Invalid Robot ID. ");
                break;
        }
    }

    public void Update()
    {
        QA1.text = "QA: " + QACounter1;
        QA2.text = "QA: " + QACounter2;
        QA3.text = "QA: " + QACounter3;
        QA4.text = "QA: " + QACounter4;

        QS1.text = "QS: " + QSCounter1;
        QS2.text = "QS: " + QSCounter2;
        QS3.text = "QS: " + QSCounter3;
        QS4.text = "QS: " + QSCounter4;
    }
}
