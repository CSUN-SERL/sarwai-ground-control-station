using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// SERVER
namespace IOStream
{
    public static class ProcessTransmitter
    {
        private static Text _timeText;

        private static double _feedOneTime,
            _feedTwoTime,
            _feedThreeTime,
            _feedFourTime;

        public static void SetupServer()
        {
            _timeText = GameObject.Find("Feed").transform.GetChild(0)
                .GetComponent<Text>();

            NetworkServer.RegisterHandler(ProcessMsgType.FeedTimeType,
                OnReceiveFeedTime);

            NetworkServer.Listen(9000);

            Debug.Log("Server initialized on port 9000...");
        }

        public static double GetFeedTime(int feed)
        {
            switch (feed)
            {
                case 0: return _feedOneTime;
                case 1: return _feedTwoTime;
                case 2: return _feedThreeTime;
                case 3: return _feedFourTime;
                default: return 0.0;
            }
        }

        private static void OnReceiveFeedTime(NetworkMessage networkMessage)
        {
            var network = networkMessage.ReadMessage<FeedTimeMessage>();
            _feedOneTime = network.FeedOne;
            _feedTwoTime = network.FeedTwo;
            _feedThreeTime = network.FeedThree;
            _feedFourTime = network.FeedFour;
        }

        public static void SendFeedChangeData(int feed, string image,
            int autonomy)
        {
            if (autonomy != 0) return;

            var msg = new FeedChangeMessage
            {
                Feed = feed,
                Image = image
            };

            NetworkServer.SendToAll(ProcessMsgType.FeedChangeType, msg);
        }

        public static void SendFeedState(int feed, int state)
        {
            var msg = new FeedStateMessage
            {
                Feed = feed,
                State = state
            };

            NetworkServer.SendToAll(ProcessMsgType.FeedStateType, msg);
        }

        public static void SendProcessTimeStep(float timeStep)
        {
            var msg = new ProcessTimeMessage
            {
                TimeScale = timeStep
            };

            NetworkServer.SendToAll(ProcessMsgType.ProcessTimeType, msg);
        }

        public static void ShutdownServer()
        {
            NetworkServer.DisconnectAll();
            //NetworkServer.Reset();
            if (NetworkServer.active)
                NetworkServer.Shutdown();

            Debug.Log("Server shutting down...");
        }
    }
}