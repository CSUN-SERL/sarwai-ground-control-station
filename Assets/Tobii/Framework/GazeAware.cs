//-----------------------------------------------------------------------
// Copyright 2016 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------

using Assets.Tobii.Framework.Internal;
using Tobii.Framework.Internal;
using UnityEngine;

namespace Assets.Tobii.Framework
{
    /// <summary>
    ///     Component that makes the game object GazeAware, meaning aware if the
    ///     participant's eye-gaze is on it or not.
    /// </summary>
    [AddComponentMenu("Eye Tracking/Gaze Aware")]
    public class GazeAware : MonoBehaviour, IGazeFocusable
    {
        public bool HasGazeFocus { get; private set; }

        /// <summary>
        ///     Function called from the gaze focus handler when the gaze focus for
        ///     this object changes. Since the implementation is explicit, it will
        ///     not be visible on instances of this component (unless cast to
        ///     <see cref="IGazeFocusable" />).
        /// </summary>
        /// <param name="hasFocus"></param>
        void IGazeFocusable.UpdateGazeFocus(bool hasFocus)
        {
            HasGazeFocus = hasFocus;
        }

        private void OnEnable()
        {
            GazeFocusHandler().RegisterFocusableComponent(this);
        }

        private void OnDisable()
        {
            GazeFocusHandler().UnregisterFocusableComponent(this);
        }

        private IRegisterGazeFocusable GazeFocusHandler()
        {
            return (IRegisterGazeFocusable) TobiiHost.GetInstance().GazeFocus;
        }
    }
}