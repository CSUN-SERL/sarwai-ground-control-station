using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;

namespace Mission.Display.Queries
{
    public class ConfidenceDisplay : MonoBehaviour
    {
        private void OnEnable()
        {
            DisplayEventManager.DisplayConfidence += DisplayConfidence;
        }

        private void OnDisable()
        {
            DisplayEventManager.DisplayConfidence -= DisplayConfidence;
        }

        private void DisplayConfidence(object sender, StringEventArgs e)
        {
            Debug.Log("Confidence: " + e.StringArgs + "%");
        }

    }
}