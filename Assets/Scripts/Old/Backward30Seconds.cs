/// <summary>
///     Responsible for rewind video from same hierarchy back by 30 seconds.
/// </summary>
/// <inheritdoc cref="AbstractVideoControlButton" />
/// <seealso cref="FeedScreen.VideoController" />
public class Backward30Seconds : AbstractVideoControlButton
{
    private LinkActionToController _backward30Seconds;

    /// <summary>
    ///     reference to fastForward button from same heirachy.
    /// </summary>
    private FastBackward _fastBackward;

    /// <summary>
    ///     reference to fastBackward button from same heirachy.
    /// </summary>
    private FastForward _fastForward;

    protected new void Start()
    {
        //links button component
        base.Start();

        // linkage with VideoController
        var feedController = gameObject.transform.parent.parent.GetChild(0)
            .GetComponent<FeedScreen.VideoController>();
        _backward30Seconds = feedController.Backward30Seconds;

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
        _backward30Seconds();
    }
}