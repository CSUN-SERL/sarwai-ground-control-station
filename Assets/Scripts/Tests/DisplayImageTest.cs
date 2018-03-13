using UnityEngine;

namespace Tests
{
    public class DisplayImageTest : MonoBehaviour
    {
        public bool On;
        public Texture TestTexture;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("space") && On)
                Mission.DisplayEventManager.OnDisplayImage(TestTexture);
        }
    }
}