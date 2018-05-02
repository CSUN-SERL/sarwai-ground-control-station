using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using FeedScreen.Experiment;
using UnityEngine;
using UnityEngine.UI;

namespace Networking
{
    public class ServerList : MonoBehaviour {

        private static Dictionary<string, IPEndPoint> _serverList = new Dictionary<string, IPEndPoint>
        {
            {"Goldshire", new IPEndPoint(IPAddress.Parse("13.56.60.57"), 8000)},
            {"Azeroth", new IPEndPoint(IPAddress.Parse("192.168.1.11"), 8000)}
        };

        public GameObject FailedTextGameObject;
        public Button ContinueButton;
        public GameObject ServerPanelPrefab;

        void OnEnable() {

            // If there are preexisting gameobjects in the list, delete them.
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }

            // Remove failed text, if there is one.
            if (FailedTextGameObject != null)
            {
                FailedTextGameObject.SetActive(false);
            }

            // Disable Continue Button.
            ContinueButton.interactable = false;

            // Populate Connection list.
            foreach (var server in _serverList) {
                print(server.Value.AddressFamily);
                var serverPanelPrefab = Instantiate(ServerPanelPrefab, transform);
                serverPanelPrefab.GetComponentInChildren<ServerSelect>().Server = server.Value;
                serverPanelPrefab.GetComponentInChildren<ServerSelect>().NameText.text = server.Key;
            }

            EventManager.ConnectionFailed += OnConnectionFailed;
            EventManager.Connect += OnConnect;
        }

        void OnDisable()
        {
            EventManager.ConnectionFailed -= OnConnectionFailed;
            EventManager.Connect -= OnConnect;
        }

        private void OnConnect(object sender, ConnectEventArgs e)
        {
            // Disable Continue Button.
            ContinueButton.interactable = false;

            if (FailedTextGameObject != null) {
                FailedTextGameObject.SetActive(false);
            }
        }

        

        private void OnConnectionFailed(object sender, EventArgs e)
        {
            // Disable Continue Button.
            ContinueButton.interactable = false;

            if (FailedTextGameObject != null) {
                FailedTextGameObject.SetActive(true);
            }
        }
    }
}
