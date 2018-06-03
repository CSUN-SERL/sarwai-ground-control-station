using Participant;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// After an experient, the proctor has the choice of pressing Q to quit the application or pressing C to restart.
/// </summary>
public class FinalSceneBehavior : MonoBehaviour
{
    // Use this for initialization
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(0);
            ParticipantBehavior.Participant = null;
        }
    }
}