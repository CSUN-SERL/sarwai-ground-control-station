using System.Collections;
using Mission.Lifecycle;
using Networking;
using Participant;
using Tobii.Plugins;
using UnityEngine;
using UnityEngine.Networking;
using EventManager = Mission.Lifecycle.EventManager;

namespace Mission
{
    /// <summary>
    ///     Responsible for loading and keeping track of the mission metadata.
    /// </summary>
    public class MissionMetaData : MonoBehaviour
    {
        public static MissionMetaData MetaDataInstance;

        public static int MissionNumber { get; set; }
        public static float MissionLength { get; set; }
        public static string MissionBrief { get; set; }

        private void Awake()
        {
            if (MetaDataInstance == null)
                MetaDataInstance = this;
            else if (MetaDataInstance != this)
                Destroy(this);
        }

        private void Start()
        {
            Load();
        }

        private void OnEnable()
        {
            EventManager.MetaDataLoaded += OnMetaDataLoaded;
        }

        private void OnDisable()
        {
            EventManager.MetaDataLoaded -= OnMetaDataLoaded;
        }

        public static void Load()
        {
            MetaDataInstance.StartCoroutine(MetaDataInstance.LoadMetaData());
        }

        private void OnMetaDataLoaded(object sender, MissionEventArgs e)
        {
            MissionLength = e.MissionLength;
            MissionBrief = e.MissionBrief;
            MissionNumber = e.MissionNumber;
        }

        private IEnumerator LoadMetaData()
        {

            Debug.Log(string.Format("Loading Mission Metadata for mission {0}", ParticipantBehavior.Participant.CurrentMission));

            var form = new WWWForm();
            form.AddField("mission_number",
                ParticipantBehavior.Participant.CurrentMission);
            using (var www = UnityWebRequest.Post(ServerURL.LOAD_MISSION, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    var result = JSON.Parse(www.downloadHandler.text);
                    if (result["failed"].AsBool)
                        Debug.Log("Could not load Mission Metadata.");

                    var data = result["data"][0];

                    var missionId = data["mission_id"].AsInt;
                    var missionLength = data["mission_length"].AsFloat;
                    var missionBrief = data["mission_brief"];

                    EventManager.OnMetaDataLoaded(missionId, missionLength,
                        missionBrief);
                }
            }
        }
    }
}