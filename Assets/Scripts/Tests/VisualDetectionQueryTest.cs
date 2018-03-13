using System.Collections;
using Mission.Queries.QueryTypes.Visual;
using UnityEngine;

namespace Tests
{
    public class DetectionQueryTest : MonoBehaviour
    {
        public Texture ImageTexture;

        public bool On;

        // Use this for initialization
        private void Start()
        {
            if (!On) return;
            StartCoroutine(SendQuery());
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("space")) StartCoroutine(SendQuery());
            ;
        }

        private IEnumerator SendQuery()
        {
            yield return new WaitForSeconds(1F);
            Debug.Log("Sending Query");
            var detectionQuery =
                new VisualDetectionQuery
                {
                    Texture = ImageTexture,
                    QueryId = 0
                };
            Mission.SocketEventManager.OnQueryRecieved(detectionQuery);
        }
    }
}