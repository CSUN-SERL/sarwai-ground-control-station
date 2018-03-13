using UnityEngine;

/// <summary>
///     Common controls that a video controller should have.
/// </summary>
public interface INterfaceVideoController
{
    /// <summary>
    ///     True if video is prepared.
    /// </summary>
    bool IsPrepared { get; }

    /// <summary>
    ///     True if video is playing, false if video is pause or not loaded.
    /// </summary>
    bool IsPlaying { get; }

    /// <summary>
    ///     true if video is finished.
    /// </summary>
    bool IsDone { get; }

    /// <summary>
    ///     The frame index being currently displayed.
    /// </summary>
    long CurrentFrameIndex { get; }

    /// <summary>
    ///     Returns total time of the video.
    /// </summary>
    double TotalTimeSeconds { get; }

    /// <summary>
    ///     Returns current duration of the video.
    /// </summary>
    ulong DurationTimeSeconds { get; }

    /// <summary>
    ///     Returns durations of the video as a percentage.
    /// </summary>
    double DurationTimePercentage { get; }

    /// <summary>
    ///     Changes video to specific time.
    /// </summary>
    /// <param name="nTime"> requires number for time in video to go to.</param>
    void SeekTimeSeconds(float nTime);

    /// <summary>
    ///     Unpauses the video player.
    /// </summary>
    void Play();

    /// <summary>
    ///     Pauses the video player.
    /// </summary>
    void Pause();

    /// <summary>
    ///     Increases playback speed of video player.
    /// </summary>
    void FastForwardStart();

    /// <summary>
    ///     Decreases playback speed to normal in video player.
    /// </summary>
    void FastForwardStop();

    /// <summary>
    ///     Rewinds the frame number by 10 frames.
    /// </summary>
    void FastBackward(float deltaTime);

    /// <summary>
    ///     Moves the video forward by 30 seconds.
    /// </summary>
    void Forward30Seconds();

    /// <summary>
    ///     Moves the vidoe backward by 30 seconds.
    /// </summary>
    void Backward30Seconds();

    /// <summary>
    ///     Mutes the sound of the video player, records current volume value.
    /// </summary>
    void Mute();

    /// <summary>
    ///     Unmutes the sound of video player to previous volume.
    /// </summary>
    void UnMute();

    /// <summary>
    ///     Gets the current frame in the video at current frame..
    /// </summary>
    /// <returns> Returns current frame.</returns>
    Texture2D GetFrameTexture2D();
}