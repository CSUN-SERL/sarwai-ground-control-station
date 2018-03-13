using UnityEngine;

namespace Mission
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource Audio;

        // Use this for initialization
        private void Start()
        {
        }

        private void OnEnable()
        {
            MissionEventManager.PlayAudioClip += PlayAudioClip;
        }

        private void PlayAudioClip(object sender, AudioEventArgs e)
        {
        }

        private void OnDisable()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}