using System.Collections;
using System.Collections.Generic;
using Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace MediaDownload
{
    public class MediaDownloader : MonoBehaviour
    {

        public const string ImageType = "texture";
        public const string AudioClipType = "audio";

        public static readonly Queue<DownloadMediaEventArgs> ExecuteOnMainThread
            = new Queue<DownloadMediaEventArgs>();

        private void OnEnable()
        {
            EventManager.DownloadMedia += OnDownloadMedia;
        }

        private void OnDisable()
        {
            EventManager.DownloadMedia -= OnDownloadMedia;
        }

        public virtual void Update()
        {
            // dispatch stuff on main thread
            while (ExecuteOnMainThread.Count > 0)
            {
                var media = ExecuteOnMainThread.Dequeue();
                switch (media.MediaType)
                {
                    case ImageType:
                        StartCoroutine(RequestImage(media));
                        break;
                    case AudioClipType:
                        StartCoroutine(RequestAudio(media));
                        break;
                }
            }
        }

        private void OnDownloadMedia(object sender, DownloadMediaEventArgs e)
        {
            ExecuteOnMainThread.Enqueue(e);
        }

        public IEnumerator RequestImage(DownloadMediaEventArgs media)
        {
            Debug.Log(string.Format("Requesting Image {0}", media.FileName));

            if (media.FileName.Length <= 0 || media.FileName == null)
                yield break;
            var www =
                UnityWebRequestTexture.GetTexture(
                    ServerURL.DownloadMediaUrl(media.FileName));
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                yield break;
            }

            var texture =
                ((DownloadHandlerTexture)www.downloadHandler).texture;
            
            var textureArgs = new DownloadTextureEventArgs
            {
                DownloadGuid = media.DownloadGuid,
                FileName = media.FileName,
                MediaType = media.MediaType,
                ImageTexture =  texture
            };
            
            Debug.Log("Image Download Successful.");

            EventManager.OnTextureDownloaded(textureArgs);

        }

        public IEnumerator RequestAudio(DownloadMediaEventArgs media)
        {

            Debug.Log(string.Format("Requesting Audio {0}", media.FileName));

            if (media.FileName.Length <= 0 || media.FileName == null)
                yield break;
            var www = UnityWebRequestMultimedia.GetAudioClip(
                ServerURL.DownloadMediaUrl(media.FileName),
                AudioType.OGGVORBIS);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                yield break;
            }


            var audioClip = ((DownloadHandlerAudioClip)www.downloadHandler)
                .audioClip;


            var audioArgs= new DownloadAudioClipEventArgs {
                DownloadGuid = media.DownloadGuid,
                FileName = media.FileName,
                MediaType = media.MediaType,
                Clip = audioClip
            };

            Debug.Log("Audio Download Successful.");

            EventManager.OnAudioClipDownloaded(audioArgs);

        }
    }
}