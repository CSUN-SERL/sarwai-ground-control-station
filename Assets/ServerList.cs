using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerList : MonoBehaviour {

    private static Dictionary<string, IPEndPoint> _serverList = new Dictionary<string, IPEndPoint>
    {
        {"Goldshire", new IPEndPoint(IPAddress.Parse("13.56.60.57"), 5901)},
        {"Azeroth", new IPEndPoint(IPAddress.Parse("192.168.1.11"), 8080)}
    };

    public GameObject ServerPanelPrefab;

    void OnEnable()
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var server in _serverList)
        {
            print(server.Value.AddressFamily);
            var serverPanelPrefab = Instantiate(ServerPanelPrefab, transform);
            serverPanelPrefab.GetComponentInChildren<ServerSelect>().Server = server.Value;
            serverPanelPrefab.GetComponentInChildren<ServerSelect>().NameText.text = server.Key;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
