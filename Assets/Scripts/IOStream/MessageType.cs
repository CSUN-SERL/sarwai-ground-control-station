using UnityEngine.Networking;

namespace IOStream
{
    public class ProcessMsgType
    {
        public static short ProcessTimeType = MsgType.Highest + 1;
        public static short FeedChangeType = MsgType.Highest + 2;
        public static short FeedTimeType = MsgType.Highest + 3;
        public static short FeedStateType = MsgType.Highest + 4;
    }

    public class ProcessTimeMessage : MessageBase
    {
        public float TimeScale;
        public string TimeString;
    }

    public class FeedChangeMessage : MessageBase
    {
        public int Feed;
        public string Image;
    }

    public class FeedTimeMessage : MessageBase
    {
        public double FeedFour;
        public double FeedOne;
        public double FeedThree;
        public double FeedTwo;
    }

    public class FeedStateMessage : MessageBase
    {
        public int Feed;
        public int State;
    }
}