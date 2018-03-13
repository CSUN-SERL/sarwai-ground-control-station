/// <inheritdoc cref="AbstractVideoControlToggle" />
/// <seealso cref="FeedScreen.VideoController" />
/// <seealso cref="FastBackward" />
/// <summary>
///     Toggles the increase of positive playback speed of VideController.
/// </summary>
public class FastForward : AbstractVideoControlToggle
{
    /// <summary>
    ///     Only for reference to fastBackward button.
    /// </summary>
    private FastBackward _fastBackward;


    public new void Start()
    {
        base.Start();
        _fastBackward = gameObject.transform.parent.transform
            .Find("FastBackward").GetComponent<FastBackward>();
    }

    /// <inheritdoc cref="AbstractVideoControlToggle.Activate" />
    /// <summary>
    ///     Stops FastBackward, and Starts FastForward
    /// </summary>
    public override void Activate()
    {
        if (_fastBackward.IsActive)
            _fastBackward.ToggleButton();
        VideoController.FastForwardStart();
    }

    /// <inheritdoc cref="AbstractVideoControlToggle.DeActivate" />
    /// <summary>
    ///     Stops FastForward.
    /// </summary>
    public override void DeActivate()
    {
        VideoController.FastForwardStop();
    }
}