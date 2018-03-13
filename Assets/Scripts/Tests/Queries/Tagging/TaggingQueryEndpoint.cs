using System.Collections;
using UnityEngine;

namespace Tests.Queries.Tagging
{
    public class TaggingQueryEndpoint : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            StartCoroutine(StartTest());
        }

        // Update is called once per frame

        private void Update()
        {
        }

        private IEnumerator StartTest()
        {
            for (var i = 0; i < 10; i++)
                yield return new WaitForSeconds(1F);
            //EventManager.OnDataRecieved("");
        }
    }
}