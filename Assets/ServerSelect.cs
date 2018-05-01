

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using FeedScreen.Experiment;
using UnityEngine;
using UnityEngine.UI;
using Ping = System.Net.NetworkInformation.Ping;

public class ServerSelect : MonoBehaviour
{

    public Text NameText;
    public Text IpPortText;

    public Image StatusImage;
    public Sprite GoodStatuSprite;
    public Sprite BadStatusSprite;

    public Button ConnectButton;

    public GameObject ServerGameObject;

    private IPEndPoint _server;

    public IPEndPoint Server
    {
        get { return _server; }
        set
        {
            _server = value;
            IpPortText.text = string.Format("{0}:{1}", value.Address, value.Port);
            CheckConnection();
        }
    }

    void CheckConnection()
    {
        //IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("139.130.4.5"), 8080);

        Ping pingSender = new Ping();
        print(Server.AddressFamily);

        PingReply reply = pingSender.Send(Server.Address);


        if (reply.Status == IPStatus.Success) {
            Console.WriteLine("Address: {0}", reply.Address);
            Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
            Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
            Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
            Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);

            StatusImage.sprite = GoodStatuSprite;
            ConnectButton.interactable = true;

        } else {
            Console.WriteLine(reply.Status);
            StatusImage.sprite = BadStatusSprite;
            ConnectButton.interactable = false;
        }
    }

    public void Connect()
    {
        // Connect the GCS Socket.
        Debug.Log("Connecting to Socket");
        Instantiate(ServerGameObject);
        SceneFlowController.LoadNextScene();
    }
}