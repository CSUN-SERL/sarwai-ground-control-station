using System;
using MediaDownload;
using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
    public class ImageDisplay : MonoBehaviour
    {
        private RawImage _image;

        public Texture DefaultImage;

        // Use this for initialization
        private void Start()
        {
            _image = gameObject.GetComponent<RawImage>();

            // Clear Display.
            DisplayEventManager.OnClearDisplay();
        }

        private void OnEnable()
        {
            DisplayEventManager.DisplayImage += DisplayImage;
            DisplayEventManager.ClearDisplay += ClearDisplay;
        }

        private void OnDisable()
        {
            DisplayEventManager.DisplayImage -= DisplayImage;
            DisplayEventManager.ClearDisplay -= ClearDisplay;
        }

        private void DisplayImage(object source, DownloadTextureEventArgs e)
        {
            _image.texture = e.ImageTexture;
        }

        private void ClearDisplay(object source, EventArgs e)
        {
            if(DefaultImage == null) return;
            _image.texture = DefaultImage;
        }
    }
}