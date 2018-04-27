using System;
using FeedScreen.Experiment;
using MediaDownload;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mission.Queries.QueryTypes.Visual
{
    public class VisualQuery : ConfidenceQuery
    {
        public Texture Texture { get; set; }
        public string ImageFileName { get; set; }

        public VisualQuery()
        {
            MediaDownload.EventManager.TextureDownloaded += OnMediaDownloaded;
        }

        public override void Display()
        {
            Debug.Log(string.Format("Query {0} being displayed", QueryId));
            base.Display();
            DisplayEventManager.OnDisplayImage(Texture);
        }

        public override void Arrive()
        {
            base.Arrive();
            MediaGuid = Guid.NewGuid();

            MediaDownload.EventManager.OnDownloadMedia(new DownloadMediaEventArgs
            {
                DownloadGuid = MediaGuid,
                FileName = ImageFileName,
                MediaType = MediaDownloader.ImageType
            });
        }

        public override string GetDisplayName()
        {
            return "Visual Detection";
        }

        public override void OnMediaDownloaded(object sender, DownloadMediaEventArgs e)
        {
            if (e.DownloadGuid != MediaGuid)
            {
                return;
            }

            var eventArgs = e as DownloadTextureEventArgs;
            Texture = eventArgs.ImageTexture;
            base.OnMediaDownloaded(sender, e);
        }
    }
}