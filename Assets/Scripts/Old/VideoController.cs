using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <inheritdoc cref="MonoBehaviour" />
/// <inheritdoc cref="INterfaceVideoController" />
/// <summary>
///     Controls all aspects of the VideoPlayer object.
/// </summary>
/// <remarks>
///     Unity VideoPlayer Script for Unity 5.6 (currently in beta 0b11 as of March 15, 2017)
///     Blog URL: http://justcode.me/unity2d/how-to-play-videos-on-unity-using-new-videoplayer/
///     YouTube Video Link: https://www.youtube.com/watch?v=nGA3jMBDjHk
///     StackOverflow Disscussion:
///     http://stackoverflow.com/questions/41144054/using-new-unity-videoplayer-and-videoclip-api-to-play-video/
///     Code Contiburation: StackOverflow - Programmer
/// </remarks>
public class VideoController : MonoBehaviour, INterfaceVideoController
{
    private const float Speed = 2;

    /// <summary>
    ///     Responsible for audio of the video.
    /// </summary>
    private AudioSource _audioSource;

    /// <seealso cref="Mute()" />
    /// <seealso cref="UnMute()" />
    /// <summary>
    ///     holds value for audio volume to use in tandum with Mute() and Unmute().
    /// </summary>
    private float _audioVolume;

    /// <summary>
    ///     Components responsible for VideoPlayer.
    /// </summary>
    private VideoPlayer _videoPlayer;

    private VideoSource _videoSource;

    /// <summary>
    ///     RawImage on which the video will play.
    /// </summary>
    public RawImage Image;

    /// <summary>
    ///     Location of video clip.
    /// </summary>
    public VideoClip VideoToPlay;


    /// <inheritdoc />
    public bool IsPrepared
    {
        get { return _videoPlayer.isPrepared; }
    }

    /// <inheritdoc />
    public bool IsPlaying
    {
        get { return _videoPlayer.isPlaying; }
    }

    /// <inheritdoc />
    public bool IsDone { get; private set; }

    /// <inheritdoc />
    public long CurrentFrameIndex
    {
        get { return _videoPlayer.frame; }
    }


    /// <inheritdoc />
    public double TotalTimeSeconds
    {
        get { return _videoPlayer.time; }
    }


    /// <inheritdoc />
    public ulong DurationTimeSeconds
    {
        get
        {
            return (ulong) (_videoPlayer.frameCount / _videoPlayer.frameRate);
        }
    }

    /// <inheritdoc />
    public double DurationTimePercentage
    {
        get { return TotalTimeSeconds / DurationTimeSeconds; }
    }

    /// <inheritdoc />
    public void Play()
    {
        if (!IsPrepared) return;
        _videoPlayer.playbackSpeed = 1;
    }

    /// <inheritdoc />
    public void Pause()
    {
        if (!IsPlaying) return;
        _videoPlayer.playbackSpeed = 0;
    }

    /// <inheritdoc />
    public void FastForwardStart()
    {
        if (!IsPlaying) return;

        _videoPlayer.playbackSpeed = Speed;
    }

    /// <inheritdoc />
    public void FastForwardStop()
    {
        if (!IsPlaying) return;

        _videoPlayer.playbackSpeed /= Speed;
    }

    /// <inheritdoc />
    public void FastBackward(float deltaTime)
    {
        if (!IsPlaying) return;
        _videoPlayer.time = _videoPlayer.time - deltaTime * Speed;
        //Backward10Frames();
    }

    /// <inheritdoc />
    public void Mute()
    {
        if (!IsPrepared) return;
        _audioVolume = _audioSource.volume;
        _audioSource.volume = 0;
        Debug.Log("audio muted");
    }

    /// <inheritdoc />
    public void UnMute()
    {
        if (!IsPrepared) return;
        _audioSource.volume = _audioVolume;
    }

    /// <inheritdoc />
    public void Forward30Seconds()
    {
        if (!IsPlaying) return;
        _videoPlayer.time = _videoPlayer.time + 30;
    }

    /// <inheritdoc />
    public void Backward30Seconds()
    {
        if (!IsPlaying) return;
        _videoPlayer.time = _videoPlayer.time - 30;
    }

    /// <inheritdoc />
    public void SeekTimeSeconds(float nTime)
    {
        if (!IsPrepared) return;
        nTime = Mathf.Clamp(nTime, 0, 1); //keeps NTime between 0 and 1
        _videoPlayer.time = nTime * DurationTimeSeconds;
    }

    /// <inheritdoc />
    public Texture2D GetFrameTexture2D()
    {
        var x = OnNewFrame();
        // var x =gameObject.GetComponent<RawImage>().texture as Texture2D;
        Debug.Log(x ? "something is correct in x" : "still no x");

        return x;
    }


    public void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(PlayVideo());
        //var stuff = videoPlayer.
    }

    /// <summary>
    ///     Responsible for setting up and playing the video.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayVideo()
    {
        //Add VideoPlayer to the GameObject
        _videoPlayer = gameObject.AddComponent<VideoPlayer>();
        //Add AudioSource
        _audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        _videoPlayer.playOnAwake = false;
        _audioSource.playOnAwake = false;
        _audioSource.Pause();
        _videoPlayer.waitForFirstFrame = true;

        //We want to play from video clip not from url
        _videoPlayer.source = VideoSource.VideoClip;
        // Vide clip from Url
        //videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";

        //Set Audio Output to AudioSource
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;


        //Assign the Audio from Video to AudioSource to be played
        _videoPlayer.EnableAudioTrack(0, true);
        _videoPlayer.SetTargetAudioSource(0, _audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        _videoPlayer.clip = VideoToPlay;
        _videoPlayer.Prepare();

        //Wait until video is prepared
        while (!_videoPlayer.isPrepared) yield return null;

        Debug.Log("Done Preparing Video");
        //Assign the Texture from Video to RawImage to be displayed
        Image.texture = _videoPlayer.texture;

        _videoPlayer.Play();
        _audioVolume = _audioSource.volume;
        Mute();

        Debug.Log("Playing Video");
        while (_videoPlayer.isPlaying)
            //Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        IsDone = true;
        Debug.Log("Done Playing Video");
    }

    /// <summary>
    ///     Rewinds VideoPlayer by 10 frames.
    /// </summary>
    public void Backward10Frames()
    {
        if (!IsPlaying) return;
        _videoPlayer.frame = _videoPlayer.frame - 10;
    }


    /// <summary>
    ///     Captures the current frame of the video player.
    /// </summary>
    /// <returns> frame as a Texture2D</returns>
    private Texture2D OnNewFrame()
    {
        var renderTexture = _videoPlayer.texture as RenderTexture;
        if (renderTexture == null) return null;
        var videoFrame =
            new Texture2D(renderTexture.width, renderTexture.height);

        RenderTexture.active = renderTexture;
        videoFrame.ReadPixels(
            new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        videoFrame.Apply();
        return videoFrame;
    }
}