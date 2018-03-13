using System;
using FeedScreen.Experiment;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using MediaDownload;
using Mission.Queries.QueryTypes.Visual;
using Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mission.Queries.QueryTypes.Audio
{
    public class AudioDetectionQuery : VisualQuery
    {
        public const string QueryType = "audio-detection";

        public AudioClip Audio { get; set; }
        public string AudioFileName { get; set; }

        public override string GetDisplayName()
        {
            return "Audio Detection";
        }

        public AudioDetectionQuery() {
            MediaDownload.EventManager.AudioClipDownloaded += OnMediaDownloaded;
        }

        public override void Display() {
            base.Display();
            DisplayEventManager.OnDisplayAudioClip(Audio);
            DisplayEventManager.OnDisplayImage(Texture);
            DisplayEventManager.OnDisplayQuestion("Can you hear a human?");
            DisplayEventManager.OnBoolQuestion(this);
        }

        public override void Arrive() {
            MediaGuid = Guid.NewGuid();

            MediaDownload.EventManager.OnDownloadMedia(new DownloadMediaEventArgs {
                DownloadGuid = MediaGuid,
                FileName = AudioFileName,
                MediaType = MediaDownloader.AudioClipType
            });
        }

        public override void OnMediaDownloaded(object sender, DownloadMediaEventArgs e) {
            if (e.DownloadGuid != MediaGuid) {
                return;
            }

            var eventArgs = e as DownloadAudioClipEventArgs;
            Audio = eventArgs.Clip;
            SocketEventManager.OnQueryRecieved(this);
        }

    }
}