using System;
using UnityEngine;
using Mission;

namespace MediaDownload
{
    public class EventManager : MonoBehaviour
    {
        public static event EventHandler<DownloadMediaEventArgs> DownloadMedia;

        public static void OnDownloadMedia(DownloadMediaEventArgs e)
        {
            Debug.Log("Download media event triggered.");
            var handler = DownloadMedia;
            if (handler != null)
                handler(null, e);
        }

        public static event EventHandler<DownloadTextureEventArgs> TextureDownloaded;

        public static void OnTextureDownloaded(DownloadTextureEventArgs e) {
            Debug.Log("Texture downloaded event triggered.");
            var handler = TextureDownloaded;
            if (handler != null)
                handler(null, e);
        }

        public static event EventHandler<DownloadAudioClipEventArgs> AudioClipDownloaded;

        public static void OnAudioClipDownloaded(DownloadAudioClipEventArgs e) {
            Debug.Log("Audio downloaded event triggered.");
            var handler = AudioClipDownloaded;
            if (handler != null)
                handler(null, e);
        }
    }
}