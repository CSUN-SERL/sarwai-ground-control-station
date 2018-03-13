//-----------------------------------------------------------------------
// Copyright 2016 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------


using Tobii.Framework.Internal;
using UnityEngine;

namespace Assets.Tobii.Framework.Internal
{
    internal interface IGazeFocus
    {
        /// <summary>
        ///     Settable camera that defines the participant's current view point.
        /// </summary>
        Camera Camera { get; set; }

        /// <summary>
        ///     Gets the <see cref="FocusedObject" /> with gaze focus. Only game
        ///     objects with a <see cref="IGazeFocusable" /> or other
        ///     <see cref="GazeAware" /> component can be focused using gaze.
        ///     <para>
        ///         Returns null if no object is focused.
        ///     </para>
        /// </summary>
        FocusedObject FocusedObject { get; }
    }
}