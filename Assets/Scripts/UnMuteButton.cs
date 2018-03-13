/// <inheritdoc cref="AbstractVideoControlToggle" />
/// <summary>
///     Toggles between mute and unmute state of the videoplayer in same heirarchy.
/// </summary>
public class UnMuteButton : AbstractVideoControlToggle
{
    /// <inheritdoc cref="AbstractVideoControlToggle.Activate" />
    /// <summary>
    ///     Starts the Unmute state.
    /// </summary>
    public override void Activate()
    {
        VideoController.UnMute();
    }

    /// <inheritdoc cref="AbstractVideoControlToggle.DeActivate" />
    /// <summary>
    ///     Starts the Mute state.
    /// </summary>
    public override void DeActivate()
    {
        VideoController.Mute();
    }
}