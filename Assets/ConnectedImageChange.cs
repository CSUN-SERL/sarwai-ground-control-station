using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Networking
{

    public class ConnectedImageChange : MonoBehaviour
    {
        private Image  _image;

        public Sprite ConnectedSprite;
        public Sprite DisconnectedSprite;
        // Use this for initialization
        void Start()
        {
            _image =gameObject.GetComponent<Image>();
            _image.sprite = DisconnectedSprite;
            Debug.Log(gameObject.name);

        }

        private void OnEnable()
        {
            EventManager.Connect += OnConnected;
            EventManager.Disconnected += OnDisconnected;
        }

        private void OnConnected(object sender, ConnectEventArgs e)
        {
            Debug.Log(gameObject.name);
            if (!_image)
                Debug.Log("image");
            if (!ConnectedSprite) Debug.Log("sprite");
            _image.sprite = ConnectedSprite;
        }

        private void OnDisconnected(object sender, EventArgs e)
        {
            _image.sprite = DisconnectedSprite;
        }

    }

}