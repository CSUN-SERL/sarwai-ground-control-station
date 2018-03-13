using System;
using UnityEngine;
using UnityEngine.UI;

/// <inheritdoc cref="MonoBehaviour" />
/// <seealso cref="VideoController" />
/// <summary>
///     abstract button with an Activate state and a DeActivate state, with a corespoinding sprite and a bool to keep track
///     of them.
/// </summary>
[Serializable]
public abstract class AbstractVideoControlToggle : MonoBehaviour
{
    /// <summary>
    ///     Reference to sprite of object.
    /// </summary>
    private Image _currentSpriteImage;

    /// <summary>
    ///     Tracks the state of button.
    /// </summary>
    private bool _isActive;

    /// <summary>
    ///     Sprite representing activated state of button.
    /// </summary>
    public Sprite Activated;
    //visual for actived button

    /// <summary>
    ///     Sprite representing deactivated state of button.
    /// </summary>
    public Sprite Deactivated;

    /// <summary>
    ///     Reference to VideoController in same hierarchy, so that its properties can be manipulated.
    /// </summary>
    protected FeedScreen.VideoController VideoController;

    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    protected void Start()
    {
        _isActive = false;
        _currentSpriteImage = gameObject.GetComponent<Image>();

        _currentSpriteImage.sprite = Deactivated;

        VideoController = gameObject.transform.parent.parent.GetChild(0)
            .GetComponent<FeedScreen.VideoController>();


        //Adds OnMouseDown listener.
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnMouseDown);
    }

    /// <summary>
    ///     Event that will active Action().
    /// </summary>
    private void OnMouseDown()
    {
        ToggleButton();
    }

    /// <summary>
    ///     Toggles Active state with DeActive state of the button.
    /// </summary>
    public void ToggleButton()
    {
        if (IsActive)
        {
            DeActivate();
            IsActive = false;
            _currentSpriteImage.sprite = Deactivated;
        }
        else
        {
            Activate();
            IsActive = true;
            _currentSpriteImage.sprite = Activated;
        }
    }


    /// <summary>
    ///     Starts the processes responsible with an active button.
    /// </summary>
    public abstract void Activate();

    /// <summary>
    ///     Stops the processes responsible with an active button.
    /// </summary>
    public abstract void DeActivate();
}