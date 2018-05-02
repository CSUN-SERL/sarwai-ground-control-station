using System;
using System.Net;
using System.Net.NetworkInformation;
using Boo.Lang;
using Networking;
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
    public GameObject LoadingCircle;
    
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

    void OnEnable()
    {
        EventManager.Connect += OnConnect;
        EventManager.ConnectionFailed += OnConnectionFailed;
        EventManager.Connected += OnConnected;
    }

    void OnDisable() {
        EventManager.Connect -= OnConnect;
        EventManager.ConnectionFailed -= OnConnectionFailed;
        EventManager.Connected -= OnConnected;
    }

    private void OnConnect(object sender, ConnectEventArgs e)
    {
        ConnectButton.interactable = false;
        if (e.EndPoint == _server)
        {
            LoadingCircle.SetActive(true);
        }
        else
        {
            return;
        }

        // Attempt connection.
        ServerConnectionBehavior.Instance.Connect(Server);
    }

    private void OnConnectionFailed(object sender, EventArgs e)
    {
        print(NameText.text);
        LoadingCircle.SetActive(false);
        ConnectButton.interactable = true;
    }

    private void OnConnected(object sender, ConnectEventArgs e)
    {
        ConnectButton.interactable = e.EndPoint != _server;
        LoadingCircle.SetActive(false);
    }

    void CheckConnection()
    {
        Ping pingSender = new Ping();
        print(Server.AddressFamily);

        PingReply reply = pingSender.Send(Server.Address);

        if (reply.Status == IPStatus.Success) {
            Debug.Log("Address: {0}" +  reply.Address);
            Debug.Log("RoundTrip time: {0}" + reply.RoundtripTime);
            Debug.Log("Time to live: {0}" +  reply.Options.Ttl);
            Debug.Log("Don't fragment: {0}" +  reply.Options.DontFragment);
            Debug.Log("Buffer size: {0}" +  reply.Buffer.Length);

            StatusImage.sprite = GoodStatuSprite;
            ConnectButton.interactable = true;

        } else {
            Debug.Log(reply.Status);
            StatusImage.sprite = BadStatusSprite;
            ConnectButton.interactable = false;
        }
    }

    public void Connect()
    {
        // Connect the GCS Socket.
        Debug.Log(string.Format("Connecting to Socket {0}:{1}", Server.Address, Server.Port));

        EventManager.OnConnect(Server);
    }
}