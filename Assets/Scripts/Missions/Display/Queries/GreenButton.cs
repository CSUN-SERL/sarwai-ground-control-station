using System;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine.UI;

namespace Mission.Display.Queries
{
    public class GreenButton : AnswerButton<int>
    {
        private void OnEnable()
        {
            DisplayEventManager.DisplayGreenButton += Display;
        }

        public override void Display(object sender, EventArgs e)
        {
            AnswerButt.targetGraphic = gameObject.GetComponent<Image>();
            gameObject.SetActive(true);
        }
    }
}