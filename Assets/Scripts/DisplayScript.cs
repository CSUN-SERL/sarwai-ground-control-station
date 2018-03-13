// Checks to see how many displays are connected and activates each.
// For our purposes, we are currently using 2 displays.
// Lines 22-24 are for the purpose of a thrid display.

using UnityEngine;

namespace LiveFeedScreen.E2SHPackage_Scripts
{
    public class DisplayScript : MonoBehaviour
    {
        private void Start()
        {
            // Display.displays[0] is the primary, default display and is always ON.
            // Check if additional displays are available and activate each.
            Debug.Log("displays connected: " + Display.displays.Length);

            // This checks to see if there is a second display, and activates it.
            if (Display.displays.Length > 1)
                Display.displays[1].Activate();

            // This checks to see if there is a thrid display and will activate that one if it exists.
            if (Display.displays.Length > 2)
                Display.displays[2].Activate();
        }
    }
}