//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

namespace Tobii.Framework
{
    /// <summary>
    ///     Represents different participant presence states.
    /// </summary>
    public enum UserPresence
    {
        /// <summary>
        ///     Participant presence is unknown.
        ///     This might be due to an error such as the eye tracker not tracking.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     The participant is present.
        /// </summary>
        Present = 1,

        /// <summary>
        ///     The participant is not present.
        /// </summary>
        NotPresent = 2
    }

    public static class UserPresenceStatusExtensions
    {
        public static bool IsUserPresent(this UserPresence userPresence)
        {
            return userPresence == UserPresence.Present;
        }
    }
}