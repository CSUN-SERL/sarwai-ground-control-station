using System;
using System.Data;
using DataCollection.Physiological;
using UnityEngine;
//Do not remove this, it breaks Unity
//using Assets.Scripts.DataCollection;

namespace Assets.Scripts.DataCollection.Physiological
{
    public class EventManager : MonoBehaviour
    {
        public static event EventHandler<EventArgs> StartLogging;

        public static void OnStartLogging()
        {
            var handler = StartLogging;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<EventArgs> StopLogging;

        public static void OnStopLogging()
        {
            var handler = StopLogging;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static event EventHandler<PhysiologicalDataEventArgs> UploadData;

        public static void OnUploadData(DataTable dataTable)
        {
            var handler = UploadData;
            if (handler != null)
                handler(null,
                    new PhysiologicalDataEventArgs {DataTable = dataTable});
        }

        public static event EventHandler<EventArgs> DataUploaded;

        public static void OnDataUploaded()
        {
            var handler = DataUploaded;
            if (handler != null) handler(null, EventArgs.Empty);
        }
    }
}