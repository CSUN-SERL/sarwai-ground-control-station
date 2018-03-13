using UnityEngine;

/// <inheritdoc cref="AbstractVideoControlToggle" />
/// <seealso cref="FeedScreen.VideoController" />
/// <seealso cref="FastForward" />
/// <summary>
///     Toggles FastBackward feature which sets the frame count back every 10 frames. Until disabled.
/// </summary>
public class FastBackward : AbstractVideoControlToggle
{
    /// <summary>
    ///     Only for reference to fastForward button.
    /// </summary>
    private FastForward _fastForward;

    /// <summary>
    ///     Allows rewind feature to work in Update().
    /// </summary>
    private bool _rewind;

    public new void Start()
    {
        base.Start();
        _fastForward = gameObject.transform.parent.transform.Find("FastForward")
            .GetComponent<FastForward>();
        //pause = gameObject.transform.parent.transform.Find("Play").GetComponent<PauseButton>();
    }

    /// <summary>
    ///     Ad hoc solution for allowing the rewind feature.
    /// </summary>
    /// TODO: make rewind feature independent of Update method, to increase performance.
    public void Update()
    {
        if (_rewind) VideoController.FastBackward(Time.deltaTime);
    }

    /// <inheritdoc cref="AbstractVideoControlToggle.Activate" />
    /// <summary>
    ///     Stops FastForward, and Starts FastBackward
    /// </summary>
    public override void Activate()
    {
        if (_fastForward.IsActive)
            _fastForward.ToggleButton();
        _rewind = true;
    }

    /// <inheritdoc cref="AbstractVideoControlToggle.DeActivate" />
    /// <summary>
    ///     Stops FastBackward.
    /// </summary>
    public override void DeActivate()
    {
        _rewind = false;
    }
}