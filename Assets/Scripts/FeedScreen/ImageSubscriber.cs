using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LiveFeedScreen.ROSBridgeLib;
using LiveFeedScreen.ROSBridgeLib.sensor_msgs.sensor_msgs;
using Tobii.Plugins;

public class ImageSubscriber : MonoBehaviour
{

    private RawImage rawimg;
    private static Texture2D texture;

    public int RobotID;
    private static int ID;

    private void Start()
    {
        rawimg = GetComponent<RawImage>();
        texture = new Texture2D(2, 2);

        ID = RobotID;
    }
    private void Update()
    {
        rawimg.texture = texture;
    }
    public new static string GetMessageTopic()
    {
        //return "/robot1/camera/rgb/image_raw/compressed";
        return "/robot4/camera/rgb/image_boxed/compressed";

        // return string.Format("/robot{0}/camera/rgb/image_raw/compressed", ID);
    }

    public new static string GetMessageType()
    {
        // return "sensor_msgs/ImageSubscriber";
        return "sensor_msgs/Image";
    }

    // Important function (I think, converting json to PoseMsg)
    public new static ROSBridgeMsg ParseMessage(JSONNode msg)
    {
        Debug.Log("ParseMessage in ImageSubscriber---------MIGUEL");
        return new ImageMsg(msg);
    }

    // This function should fire on each ros message
    public new static void CallBack(ROSBridgeMsg msg)
    {
        Debug.Log(GetMessageTopic() + " received");
        ImageMsg imageMsg = (ImageMsg)msg;
        Debug.Log("HERE IS THE BYTE[] COMMING IN FROM ROS");
        Debug.Log("------------------------------------------");
        byte[] arr;
        arr = imageMsg.GetImage();
        Debug.Log(imageMsg.GetImage());
        Debug.Log(arr[1]);
        Debug.Log("------------------------------------------");

        byte[] bimage = imageMsg.GetImage();
        texture.LoadImage(bimage);
        //texture.LoadRawTextureData(bimage);
        texture.Apply();


        // GUI.DrawTexture(new Rect(10, 10, 60, 60), texture, ScaleMode.ScaleToFit, true, 10.0F);
        //GetComponent<Renderer>().material.mainTexture = texture;
    }
}
