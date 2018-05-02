using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Networking
{

    public class ServerConnectContinue : MonoBehaviour
    {


        private Queue<bool> buttonStateQueue;

        void OnEnable() {
            EventManager.Connect += OnConnect;
            EventManager.Connected += OnConnected;
            buttonStateQueue = new Queue<bool>();
        }

        private void OnConnect(object sender, ConnectEventArgs e)
        {
            buttonStateQueue.Enqueue(false);
        }

        void OnDisable() {
            EventManager.Connect += OnConnect;
            EventManager.Connected -= OnConnected;
        }

        private void OnConnected(object sender, EventArgs e) {
            buttonStateQueue.Enqueue(true);   
        }

        void Update()
        {
            if (buttonStateQueue.Count > 0)
            {
                GetComponent<Button>().interactable = buttonStateQueue.Dequeue();
            }
        }
    }
}
