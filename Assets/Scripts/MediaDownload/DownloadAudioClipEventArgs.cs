using UnityEngine;

namespace MediaDownload {
    public class DownloadAudioClipEventArgs : DownloadMediaEventArgs {
        public AudioClip Clip { get; set; }
    }
}