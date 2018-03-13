using UnityEngine;

namespace Mission.ManualControl
{
    public class ManualControlTest : MonoBehaviour
    {
        public bool On;

        // Update is called once per frame
        private void Update()
        {
            if (!On) return;

            if (Input.GetKeyDown("1")) EventManager.OnManualControlEnable(1);
            if (Input.GetKeyDown("2")) EventManager.OnManualControlEnable(2);
            if (Input.GetKeyDown("3")) EventManager.OnManualControlEnable(3);
            if (Input.GetKeyDown("4")) EventManager.OnManualControlEnable(4);
        }
    }
}