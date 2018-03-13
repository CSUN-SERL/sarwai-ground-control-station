using System;
namespace MediaDownload
{
    public class DownloadMediaEventArgs : EventArgs
    {
        public Guid DownloadGuid { get; set; }
        public string MediaType { get; set; }
        public string FileName { get; set; }
    }
}