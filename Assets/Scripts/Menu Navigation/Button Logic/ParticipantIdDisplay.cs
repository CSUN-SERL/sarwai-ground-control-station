using Participant;
using UnityEngine;
using UnityEngine.UI;

public class ParticipantIdDisplay : MonoBehaviour
{

    private Text _participantIdText;

    private void OnEnable()
    {
        EventManager.NewParticipantMade += OnNewParticipantMade;
    }

    private void OnDisable()
    {
        EventManager.NewParticipantMade -= OnNewParticipantMade;
    }

    private void OnNewParticipantMade(object sender, NewParticipantEventArgs e)
    {
        _participantIdText = GetComponent<Text>();
        _participantIdText.text = "Participant Id: " + e.Data.Id;
    }
    
}
