using Networking;
using UnityEngine;

namespace Tests
{
    public class JsonStressTest1 : MonoBehaviour
    {
        public bool On;

        // Use this for initialization
        private void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown("space") && On)
            {
                var p = QueryJsonFactory.MakeQueryJson("visual-detection");

                GcsSocket.Emit("gcs-query-received", p);
            }

            ;
        }
    }
}