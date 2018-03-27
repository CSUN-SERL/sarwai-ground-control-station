using UnityEngine;
using System.Collections;
using LiveFeedScreen.ROSBridgeLib.std_msgs.std_msgs;
public class CoffeePublisher
{
    public static string GetMessageType()
    {
        return StringMsg.GetMessageType();
    }

    public static string GetMessageTopic()
    {
        return "missedDetections";
    }
}