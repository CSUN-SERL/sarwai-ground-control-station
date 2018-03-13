using System;

namespace Participant
{
    public class NewParticipantEventArgs : EventArgs
    {
        public ParticipantData Data { get; set; }
    }
}