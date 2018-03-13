using UnityEngine;

namespace MediaDownload {
    public class DownloadTextureEventArgs : DownloadMediaEventArgs {
        public Texture ImageTexture { get; set; }
    }
}