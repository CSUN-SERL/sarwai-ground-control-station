using UnityEngine;

public interface INterfaceSiri
{
    /// <summary>
    ///     True if video is prepared.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <returns> True if nFeed video was loaded.</returns>
    bool IsPrepared(int nFeed);

    /// <summary>
    ///     True if video is playing, false if video is pause or not loaded.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <returns> True if nFeed video is currently playing.</returns>
    bool IsPlaying(int nFeed);

    /// <summary>
    ///     true if video is finished.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <returns> True if nFeed video is done playing.</returns>
    bool IsDone(int nFeed);

    /// <summary>
    ///     The frame index being currently displayed.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <returns> Number of frames passed in nFeed video since start.</returns>
    long CurrentFrameIndex(int nFeed);

    /// <summary>
    ///     Returns total time of the video.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <returns> Total time of the nFeed video in seconds.</returns>
    double TotalTimeSeconds(int nFeed);

    /// <summary>
    ///     Returns current duration of the video.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <returns> Current duration in nFeed video.</returns>
    ulong DurationTimeSeconds(int nFeed);

    /// <summary>
    ///     Returns durations of the video as a percentage.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <returns> Current duration in nFeed video as a percentage.</returns>
    double DurationTimePercentage(int nFeed);


    /// <summary>
    ///     Unpauses the video player.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void Play(int nFeed);

    /// <summary>
    ///     Pauses the video player.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void Pause(int nFeed);

    /// <summary>
    ///     Increases playback speed of video player.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void FastForwardStart(int nFeed);

    /// <summary>
    ///     Decreases playback speed to normal in video player.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void FastForwardStop(int nFeed);

    /// <summary>
    ///     Start rewind where VideoPlayer decreases the frame number by 10 frames continuously.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void FastBackwardStart(int nFeed);

    /// <summary>
    ///     Stop the rewind process.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void FastBackwardStop(int nFeed);

    /// <summary>
    ///     Moves the video forward by 30 seconds.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void Forward30Seconds(int nFeed);

    /// <summary>
    ///     Moves the vidoe backward by 30 seconds.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void Backward30Seconds(int nFeed);

    /// <summary>
    ///     Mutes the sound of the video player, records current volume value.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void Mute(int nFeed);

    /// <summary>
    ///     Unmutes the sound of video player to previous volume.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    void UnMute(int nFeed);

    /// <summary>
    ///     Changes video to specific time.
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <param name="nTime"> requires number for time in video to go to.</param>
    void SeekTimeSeconds(int nFeed, float nTime);

    /// <summary>
    ///     Gets the current frame in the video at current frame..
    /// </summary>
    /// <param name="nFeed"> Feed number, 0:4</param>
    /// <returns> Returns current frame.</returns>
    Texture2D GetFrameTexture2D(int nFeed);
}