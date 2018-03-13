using System;
using System.Collections;
using Assets.Scripts.DataCollection.Physiological;
using UnityEngine;

namespace Assets.Scripts.DataCollection
{
    public class PictureTaker : MonoBehaviour
    {
        private WebCamTexture _webCamTexture;

        private void OnEnable()
        {
            EventManager.StartLogging += StartLogging;
            EventManager.StopLogging += StopLogging;
        }

        private void OnDisable()
        {
            EventManager.StartLogging -= StartLogging;
            EventManager.StopLogging -= StopLogging;
        }

        private void StartLogging(object sender, EventArgs e)
        {
            Debug.Log("Starting pictures...");
            _webCamTexture = new WebCamTexture();
            _webCamTexture.Play();
            StartCoroutine(TakePictures());
        }

        private void StopLogging(object sender, EventArgs e)
        {
            Debug.Log("Stopping pictures...");
            _webCamTexture.Stop();
            _webCamTexture = null;
            StopCoroutine(TakePictures());
        }

        private IEnumerator TakePictures()
        {
            yield return new WaitForSeconds(1F);
            Debug.Log("Taking Picture.");
        }
    }
}