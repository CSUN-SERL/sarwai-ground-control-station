using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// CLIENT
namespace IOStream
{
    public class ProcessReceiver : MonoBehaviour
    {
        private NetworkClient _networkClient;
        private Text _timeText;

        public Siri SiriVar;

        // Use this for initialization
        private void Start()
        {
            Debug.Log("Setting up client...");
            _timeText = GameObject.Find("Canvas").transform.Find("Time")
                .GetComponent<Text>();
            SetupClient();
        }

        private void Test()
        {
            var screenDuration = SiriVar.DurationTimeSeconds(1);
        }

        private void SetupClient()
        {
            _networkClient = new NetworkClient();
            _networkClient.RegisterHandler(MsgType.Connect, OnConnected);
            _networkClient.RegisterHandler(ProcessMsgType.ProcessTimeType,
                OnReceiveTimeMessage);
            _networkClient.RegisterHandler(ProcessMsgType.FeedChangeType,
                OnReceiveFeedMessage);
            _networkClient.RegisterHandler(ProcessMsgType.FeedStateType,
                OnReceiveFeedState);

            _networkClient.Connect("127.0.0.1", 9000);

            //Time.timeScale = 0;
        }

        private static void OnConnected(NetworkMessage networkMessage)
        {
            Debug.Log("Connected to server");

            //Time.timeScale = 1;
        }

        private void OnReceiveTimeMessage(NetworkMessage networkMessage)
        {
            //Debug.Log("Recieved Time");
            //Time.timeScale = networkMessage.ReadMessage<ProcessTimeMessage>().TimeScale;
            _timeText.text = networkMessage.ReadMessage<ProcessTimeMessage>()
                .TimeString;
        }

        private static void OnReceiveFeedMessage(NetworkMessage networkMessage)
        {
            //var network = networkMessage.ReadMessage<FeedChangeMessage>();
            //var tempGameObject = GameObject.Find(network.Name).transform.GetChild(0).GetChild(0);
            //tempGameObject.GetComponent<Text>().text = network.Text != "" ? network.Text : "Image";
        }

        private void OnReceiveFeedState(NetworkMessage networkMessage)
        {
            var network = networkMessage.ReadMessage<FeedStateMessage>();

            if (network.State == 0) SiriVar.Pause(network.Feed);
            else SiriVar.Play(network.Feed);
        }

        private void Update()
        {
            if (!_networkClient.isConnected) return;

            var msg = new FeedTimeMessage
            {
                FeedOne = SiriVar.TotalTimeSeconds(0),
                FeedTwo = SiriVar.TotalTimeSeconds(1),
                FeedThree = SiriVar.TotalTimeSeconds(2),
                FeedFour = SiriVar.TotalTimeSeconds(3)
            };

            _networkClient.Send(ProcessMsgType.FeedTimeType, msg);
        }
    }
}