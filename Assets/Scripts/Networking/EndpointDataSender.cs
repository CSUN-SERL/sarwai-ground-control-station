using System.Collections.Generic;
using Mission;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Networking
{
    public class EndpointDataSender : MonoBehaviour
    {
        private void OnEnable()
        {
            MissionEventManager.QueryAnswered += UploadQueryAnswer;
        }

        private void OnDisable()
        {
            MissionEventManager.QueryAnswered -= UploadQueryAnswer;
        }

        public static void UploadQueryAnswer(object sender, QueryEventArgs eventArgs)
        {

            var answerTime = System.DateTime.Now;

            Debug.Log(string.Format("Sending Query Answer: QID: {0} Answer: {1} Time answered: {2}", eventArgs.Query.QueryId, 
                eventArgs.Query.Response, answerTime));


            // TODO Upload proper datetime.
            var responseDict =
                new Dictionary<string, int>
                {
                    {"query_id", eventArgs.Query.QueryId},
                    {"response", eventArgs.Query.Response},
                    {"answerTime", answerTime.Minute}
                };


            var responseJson = JsonConvert.SerializeObject(responseDict, Formatting.Indented);

            Debug.Log(responseJson);

            GcsSocket.Emit(ServerURL.SEND_ANSWER_QUERY,
                responseJson);
        }
    }
}