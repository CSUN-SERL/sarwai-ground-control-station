using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Tests
{
    public class PictureTest : MonoBehaviour
    {
        private WebCamTexture _webcamtex;
        public bool On;


        private void Start()
        {
            if (!On) return;
            _webcamtex = new WebCamTexture();
            _webcamtex.Play();
        }

        private void Update()
        {
            if (!On) return;
            GetComponent<RawImage>().texture = _webcamtex;
            if (Input.GetKeyDown(KeyCode.A))
                StartCoroutine(CaptureTextureAsPNG());
        }

        private IEnumerator CaptureTextureAsPNG()
        {
            yield return new WaitForEndOfFrame();
            var textureFromCamera = new Texture2D(
                GetComponent<RawImage>().texture.width,
                GetComponent<RawImage>().texture.height);
            textureFromCamera.SetPixels(
                ((WebCamTexture) GetComponent<RawImage>().texture).GetPixels());
            textureFromCamera.Apply();
            var bytes = textureFromCamera.EncodeToPNG();
            var filePath = "SavedScreen1.png";

            File.WriteAllBytes(filePath, bytes);
            Debug.Log("File Saved");
        }
    }
}