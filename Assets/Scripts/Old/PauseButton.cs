/// <inheritdoc cref="AbstractVideoControlToggle" />
/// <seealso cref="FastForward" />
/// <seealso cref="FastBackward" />
/// <summary>
///     Toggles pause and play of VideoController.
/// </summary>
public class PauseButton : AbstractVideoControlToggle
{
    /// <summary>
    ///     Only for reference to FastForward Button.
    /// </summary>
    private FastBackward _fastBackward;

    /// <summary>
    ///     Only for reference to FastBackward Button.
    /// </summary>
    private FastForward _fastForward;

    public new void Start()
    {
        base.Start();

        _fastForward = gameObject.transform.parent.transform.Find("FastForward")
            .GetComponent<FastForward>();
        _fastBackward = gameObject.transform.parent.transform
            .Find("FastBackward").GetComponent<FastBackward>();
    }

    /// <inheritdoc />
    /// <summary>
    ///     Pauses whatever previous option of playback was active.
    /// </summary>
    /// TODO: implement a way to keep track of which state of playback speed was paused. So that you can resume with same state.
    public override void Activate()
    {
        if (_fastForward.IsActive)
            _fastForward.DeActivate();
        else if (_fastBackward.IsActive)
            _fastBackward.DeActivate();

        VideoController.Pause();
    }

    /// <inheritdoc />
    /// <summary>
    ///     Resumes whatever previous option of playback was active.
    /// </summary>
    public override void DeActivate()
    {
        VideoController.Play();
    }
}