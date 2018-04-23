using System;

namespace Participant
{

    /// <summary>
    /// Event arguments to hold data for participant creation.
    /// </summary>
    public class NewParticipantEventArgs : EventArgs
    {
        public ParticipantData Data { get; set; }
    }
}