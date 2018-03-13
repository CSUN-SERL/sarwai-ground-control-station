using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Used to encapsulate button functions.
/// </summary>
/// <inheritdoc cref="MonoBehaviour" />
[Serializable]
public abstract class AbstractVideoControlButton : MonoBehaviour
{
    /// <summary>
    ///     requires a link to the video controller.
    /// </summary>
    public delegate void LinkActionToController();


    protected void Start()
    {
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnMouseDown);
    }

    /// <summary>
    ///     Event that will active Action().
    /// </summary>
    private void OnMouseDown()
    {
        Action();
    }

    /// <summary>
    ///     Requires action that the button will perform when pressed.
    /// </summary>
    public abstract void Action();
}