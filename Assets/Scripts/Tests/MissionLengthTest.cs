using Mission.Lifecycle;
using UnityEngine;

namespace Tests
{
    public class MissionLengthTest : MonoBehaviour
    {
        public string Brief;
        public float Length;
        public int MissionNumber;
        public bool On;


        private void Start()
        {
            if (!On) return;
            EventManager.OnMetaDataLoaded(MissionNumber, Length, Brief);
        }
    }
}