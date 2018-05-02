using System.Collections;
using System.Collections.Generic;
using Participant;
using UnityEngine;
using UnityEngine.UI;

public class GroupButton : MonoBehaviour
{

    private static int n_buttons;
    private int group_num;
    private Button _button;

    private Dictionary<int, string> groups = new Dictionary<int, string>
    {
        { 1, "Group 1\nAT"},
        { 2, "Group 2\nANT"},
        { 3, "Group 3\nNANT"}
    };

    public void Awake()
    {
        EventManager.NewParticipantMade += OnNewParticipantMade;

        // Add listener to make participant.
        _button = GetComponent<Button>();
        _button.onClick.AddListener(MakeNewParticipant);


        // Reset
        n_buttons = 0;

        // Assign group number to button.
        group_num = ++n_buttons;
        _button.GetComponentInChildren<Text>().text = groups[group_num];
    }

    public void OnDestroy()
    {
        EventManager.NewParticipantMade -= OnNewParticipantMade;
    }

    private void MakeNewParticipant()
    {
        ParticipantBehavior.Instance.MakeNewParicipant(group_num);
    }

    private void OnNewParticipantMade(object sender, NewParticipantEventArgs e)
    {
        GetComponent<Button>().interactable = false;
    }
}
