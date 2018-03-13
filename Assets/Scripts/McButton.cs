using System.Collections.Generic;
using UnityEngine;

/// <inheritdoc cref="AbstractVideoControlToggle" />
/// <seealso cref="PauseButton" />
/// <summary>
///     Responsible for accessing manual control operation for robots.
/// </summary>
/// TODO: Implement actual controls, and not just "demo" controls
public class McButton : AbstractVideoControlToggle
{
    /// <summary>
    ///     Keeps reference of other manual control operations.
    /// </summary>
    private List<McButton> _feeds;

    /// <summary>
    ///     Reference to pause button, so that video can be paused when Active().
    /// </summary>
    private PauseButton _pause;

    public new void Start()
    {
        base.Start();

        _pause = gameObject.transform.parent.transform.Find("Play")
            .GetComponent<PauseButton>();

        _feeds = new List<McButton>
        {
            GameObject.Find("Feed0").transform.GetChild(1).Find("ManualButton")
                .GetComponent<McButton>(),
            GameObject.Find("Feed1").transform.GetChild(1).Find("ManualButton")
                .GetComponent<McButton>(),
            GameObject.Find("Feed2").transform.GetChild(1).Find("ManualButton")
                .GetComponent<McButton>(),
            GameObject.Find("Feed3").transform.GetChild(1).Find("ManualButton")
                .GetComponent<McButton>()
        };
    }

    /// <inheritdoc cref="AbstractVideoControlToggle.Activate()" />
    /// <summary>
    ///     Activates Manual Control, deactivate all other instances of Manual Control.
    /// </summary>
    /// TODO: keep livefeed active, (do when livefeed is incorporated)
    public override void Activate()
    {
        if (_pause.IsActive == false)
            _pause.Activate();
        foreach (var mc in _feeds)
            if (mc.IsActive)
                mc.ToggleButton();
    }

    /// <inheritdoc cref="AbstractVideoControlToggle.DeActivate()" />
    /// <summary>
    ///     DeActivtes Manual Control, resumes play.
    /// </summary>
    /// TODO: keep livefeed active, perhaps go back to where video was previously, when manual control started.
    public override void DeActivate()
    {
        _pause.DeActivate();
    }
}