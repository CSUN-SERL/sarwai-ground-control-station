using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Tests
{
    public class JSONTest : MonoBehaviour
    {
        public bool On;

        // Use this for initialization
        private void Start()
        {
            if (!On) return;
            var foo = new List<List<string>>
            {
                new List<string> {"Hello", "World"},
                new List<string> {"My name", "is Collin"}
            };
            var json = JsonConvert.SerializeObject(foo);
            Debug.Log(json);
            Debug.Log(JsonConvert.DeserializeObject(json));
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}