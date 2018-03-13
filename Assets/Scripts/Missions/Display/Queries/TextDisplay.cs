using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Display.Queries
{
    public class TextDisplay : MonoBehaviour
    {
        private Text _text;

        private void OnEnable()
        {
            _text = gameObject.GetComponent<Text>();

            DisplayEventManager.DisplayQuestion += DisplayText;
            DisplayEventManager.ClearDisplay += Clear;
        }

        private void OnDisable()
        {
            DisplayEventManager.DisplayQuestion -= DisplayText;
            DisplayEventManager.ClearDisplay -= Clear;
        }

        private void DisplayText(object sender, StringEventArgs e)
        {
            _text.text = e.StringArgs;
        }

        private void Clear(object sender, EventArgs e)
        {
            _text.text = "";
        }
    }
}