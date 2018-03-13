using UnityEngine;
using UnityEngine.UI;

//using NUnit.Framework.Constraints;
//using UnityEditor.Rendering;

namespace Mission.Display
{
    public class TimerDisplay : MonoBehaviour
    {
        private Text _text;

        private string minutes;
        private string seconds;

        private float timer;

        // Use this for initialization
        private void Start()
        {
            _text = gameObject.GetComponent<Text>();
            _text.text = "0 seconds";
        }

        // Update is called once per frame
        private void Update()
        {
            timer = MissionTimer.CurrentTime;

            minutes = Mathf.Floor(timer / 60).ToString("00");
            seconds = (timer % 60).ToString("00");
            _text.text =
                string.Format("Current Time: " + minutes + ":" + seconds);
        }
    }
}