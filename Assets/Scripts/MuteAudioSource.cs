// Allows for mute functionality for ever mute button on the application.

using UnityEngine;
using UnityEngine.UI;

namespace LiveFeedScreen.E2SHPackage_Scripts
{
    public class MuteAudioSource : MonoBehaviour
    {
        private AudioSource _audioSource;

        // Allows you to chose which button to implement the script on. 
        // Done though the Unity inspector.
        public Button YourButton;

        private void Start()
        {
            // Initializes variable audioSource with a live audiosource that is in the given scene.
            _audioSource = GetComponent<AudioSource>();

            // Initializes variable audioSource with a live audiosource that is in the given scene.
            var btn = YourButton.GetComponent<Button>();

            // Tell the button what to do when it is being clicked on. Here, it calls the function TaskOnClick.
            btn.onClick.AddListener(TaskOnClick);
        }

        // Update is called automatically at every frame of the game.
        private void Update()
        {
            // Tells the audioSource variable to toggle the mute functionality of the AudioSource that is assigned to the button, 
            // whenever the 'M' button is pressed. 
            if (Input.GetKeyDown(KeyCode.M))
                _audioSource.mute = !_audioSource.mute;
        }

        private void TaskOnClick()
        {
            // Tells the audioSource variable to toggle the mute functionality of the AudioSource that is assigned to the button.
            _audioSource.mute = !_audioSource.mute;
        }
    }
}