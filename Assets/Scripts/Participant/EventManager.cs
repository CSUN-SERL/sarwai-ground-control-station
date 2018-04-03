using System;
using UnityEngine;

namespace Participant
{
    public class EventManager : MonoBehaviour
    {
        public static event EventHandler<NewParticipantEventArgs> MakeNewParticipant;

        public static void OnMakeNewParticipant(NewParticipantEventArgs e)
        {
            var handler = MakeNewParticipant;
            if (handler != null) handler(null, e);
        }

        public static event EventHandler<EventArgs>
            NewParticipantMade;

        public static void OnNewParticipantMade()
        {
            var handler = NewParticipantMade;
            if (handler != null) handler(null, new EventArgs());
        }
    }
}