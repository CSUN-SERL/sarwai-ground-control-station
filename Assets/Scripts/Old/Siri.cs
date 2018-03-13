using System.Collections.Generic;
using UnityEngine;

public class Siri : MonoBehaviour, INterfaceSiri
{
    private List<FeedScreen.VideoController> _feedManagers;

    /// <inheritdoc cref="INterfaceSiri" />
    public bool IsPrepared(int nFeed)
    {
        return _feedManagers[nFeed].IsPrepared;
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public bool IsPlaying(int nFeed)
    {
        return _feedManagers[nFeed].IsPlaying;
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public bool IsDone(int nFeed)
    {
        return _feedManagers[nFeed].IsDone;
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public long CurrentFrameIndex(int nFeed)
    {
        return _feedManagers[nFeed].CurrentFrameIndex;
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public double TotalTimeSeconds(int nFeed)
    {
        return _feedManagers[nFeed].TotalTimeSeconds;
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public ulong DurationTimeSeconds(int nFeed)
    {
        return _feedManagers[nFeed].DurationTimeSeconds;
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public double DurationTimePercentage(int nFeed)
    {
        return _feedManagers[nFeed].DurationTimePercentage;
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void Play(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(0)
            .GetComponent<PauseButton>().DeActivate();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void Pause(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(0)
            .GetComponent<PauseButton>().Activate();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void FastForwardStart(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(1).GetComponent<FastForward>().Activate();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void FastForwardStop(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(1).GetComponent<FastForward>().DeActivate();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void FastBackwardStart(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(2).GetComponent<FastBackward>().Activate();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void FastBackwardStop(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(2).GetComponent<FastBackward>().DeActivate();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void Forward30Seconds(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(3)
            .GetComponent<Forward30Seconds>().Action();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void Backward30Seconds(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(4)
            .GetComponent<Backward30Seconds>().Action();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void Mute(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(5)
            .GetComponent<UnMuteButton>().Activate();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void UnMute(int nFeed)
    {
        GameObject.Find(string.Format("Feed{0}", nFeed)).transform.GetChild(1)
            .GetChild(5)
            .GetComponent<UnMuteButton>().DeActivate();
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public void SeekTimeSeconds(int nFeed, float nTime)
    {
        _feedManagers[nFeed].SeekTimeSeconds(nTime);
    }

    /// <inheritdoc cref="INterfaceSiri" />
    public Texture2D GetFrameTexture2D(int nFeed)
    {
        return _feedManagers[nFeed].GetFrameTexture2D();
    }

    // Use this for initialization
    public void Start()
    {
        _feedManagers = new List<FeedScreen.VideoController>
        {
            GameObject.Find("Feed0").transform.GetChild(0)
                .GetComponent<FeedScreen.VideoController>(),
            GameObject.Find("Feed1").transform.GetChild(0)
                .GetComponent<FeedScreen.VideoController>(),
            GameObject.Find("Feed2").transform.GetChild(0)
                .GetComponent<FeedScreen.VideoController>(),
            GameObject.Find("Feed3").transform.GetChild(0)
                .GetComponent<FeedScreen.VideoController>()
        };

        // //testing
        // UpdateLiveFeed (1, Directory.GetCurrentDirectory () + "/Assets/Sprites/SERL-Wallpaper-3840x2160.png");
        // UpdateLiveFeed (2, Directory.GetCurrentDirectory () + "/Assets/Sprites/SERL-Wallpaper-3840x2160.png");
        // UpdateLiveFeed (3, Directory.GetCurrentDirectory () + "/Assets/Sprites/SERL-Wallpaper-3840x2160.png");
        // UpdateLiveFeed (4, Directory.GetCurrentDirectory () + "/Assets/Sprites/SERL-Wallpaper-3840x2160.png");
    }
}