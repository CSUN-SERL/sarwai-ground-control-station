using System;
using System.Collections;
using UnityEngine;

namespace DataCollection
{
    public class DataCounter : MonoBehaviour {

        private int dataCounter;
        private int totalBytes;

        // Use this for initialization
        void Start() {
            StartCoroutine(Counter());
        }

        // Update is called once per frame
        void Update() {

        }

        IEnumerator Counter() {
            while (true) {
                dataCounter = totalBytes;
                yield return new WaitForSeconds(1);
                print((totalBytes - dataCounter) * 1e-6 + "Mbps");
                print("Total: " + totalBytes + " bytes streamed");
            }
        }

        void OnEnable() {
            DataDownloaded += Count;
        }

        private void Count(object sender, IntEventArgs e) {
            //print(string.Format("{0} + {1} = {2}", totalBytes, e.IntField, totalBytes + e.IntField));
            totalBytes += e.IntField;
        }

        public static event EventHandler<IntEventArgs> DataDownloaded;

        public static void OnDataDownloaded(int numBytes) {
            var handler = DataDownloaded;
            if (handler != null) handler(null, new IntEventArgs {
                IntField = numBytes
            });
        }
    }

    public class IntEventArgs : EventArgs {
        public int IntField { get; set; }
    }

}
