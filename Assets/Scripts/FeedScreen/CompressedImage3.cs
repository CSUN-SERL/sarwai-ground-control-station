using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LiveFeedScreen.ROSBridgeLib;
using LiveFeedScreen.ROSBridgeLib.sensor_msgs.sensor_msgs;
using Tobii.Plugins;
public class CompressedImage3 : MonoBehaviour
{
    private RawImage rawimg;
    private static Texture2D texture;
    [SerializeField]
    const int BotNum = 1;
    private void Start()
    {
        rawimg = GetComponent<RawImage>();
        texture = new Texture2D(2, 2);

    }
    private void Update()
    {
        rawimg.texture = texture;
    }
    public new static string GetMessageTopic()
    {
        return "/robot3/camera/rgb/image_raw/compressed";
    }

    public new static string GetMessageType()
    {
        return "sensor_msgs/CompressedImage";
    }

    // Important function (I think, converting json to PoseMsg)
    public new static ROSBridgeMsg ParseMessage(JSONNode msg)
    {
        Debug.Log("ParseMessage in CompressedImage---------MIGUEL");
        return new CompressedImageMsg(msg);
    }

    // This function should fire on each ros message
    public new static void CallBack(ROSBridgeMsg msg)
    {
        Debug.Log(GetMessageTopic() + " received");
        CompressedImageMsg imageMsg = (CompressedImageMsg)msg;
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
