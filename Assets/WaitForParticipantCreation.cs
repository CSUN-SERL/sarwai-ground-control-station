using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Participant;
using System;
using UnityEngine.UI;

public class WaitForParticipantCreation : MonoBehaviour {


	/// <summary>
	///     In the event that the server takes too long to resposd with
	///     from a coroutine, we will now wait until the coroutine has 
	/// </summary>
	void Start () {
	}
	
	public void Awake()
	{
		EventManager.NewParticipantMade += OnNewParticipantMade;
	}

	public void OnDestroy()
	{
		EventManager.NewParticipantMade -= OnNewParticipantMade;
	}
	/// <summary>
	///   We will now wait until an instance of a participant object 
	///   has been created before we allow movement to the next scene
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public void OnNewParticipantMade(object sender, EventArgs e)
	{
		GetComponent<Button>().interactable = true;
	}
}
