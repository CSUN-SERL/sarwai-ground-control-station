/// <summary>
///     Responsible for rewind video from same hierarchy back by 30 seconds.
/// </summary>
/// <inheritdoc cref="AbstractVideoControlButton" />
/// <seealso cref="FeedScreen.VideoController" />
public class Forward30Seconds : AbstractVideoControlButton
{
    /// <summary>
    ///     reference to fastForward button from same heirachy.
    /// </summary>
    private FastBackward _fastBackward;

    /// <summary>
    ///     reference to fastBackward button from same heirachy.
    /// </summary>
    private FastForward _fastForward;

    private LinkActionToController _forward30Seconds;

    protected new void Start()
    {
        //links button component
        base.Start();

        // linkage with VideoController
        var feedController = gameObject.transform.parent.parent.GetChild(0)
            .GetComponent<FeedScreen.VideoController>();
        _forward30Seconds = feedController.Forward30Seconds;

        _fastForward = gameObject.transform.parent.transform.Find("FastForward")
            .GetComponent<FastForward>();
        _fastBackward = gameObject.transform.parent.transform
            .Find("FastBackward").GetComponent<FastBackward>();
    }

    /// <inheritdoc cref="FastForward" />
    /// <inheritdoc cref="FastBackward" />
    /// <summary>
    ///     Stops FastForward and FastBackward if they are active, then rewinds VideoController by 30 seconds.
    /// </summary>
    public override void Action()
    {
        if (_fastForward.IsActive)
            _fastForward.DeActivate();
        if (_fastBackward.IsActive)
            _fastBackward.DeActivate();
        _forward30Seconds();
    }
}