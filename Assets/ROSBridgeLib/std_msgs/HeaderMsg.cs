using Tobii.Plugins;
using UnityEngine;

/**
 * Define a header message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 */

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class HeaderMsg : ROSBridgeMsg
        {
            private readonly string _frame_id;
            private readonly int _seq;
            private readonly TimeMsg _stamp;

            public HeaderMsg(JSONNode msg)
            {
                Debug.Log("HeaderMsg with " + msg);
                _seq = int.Parse(msg["seq"]);
                _stamp = new TimeMsg(msg["stamp"]);
                _frame_id = msg["frame_id"];
                Debug.Log("HeaderMsg done ");
                Debug.Log(" and it looks like " + ToString());
            }

            public HeaderMsg(int seq, TimeMsg stamp, string frame_id)
            {
                _seq = seq;
                _stamp = stamp;
                _frame_id = frame_id;
            }

            public static string GetMessageType()
            {
                return "std_msgs/Header";
            }

            public int GetSeq()
            {
                return _seq;
            }

            public TimeMsg GetTimeMsg()
            {
                return _stamp;
            }

            public string GetFrameId()
            {
                return _frame_id;
            }

            public override string ToString()
            {
                return "Header [seq=" + _seq + ",  stamp=" + _stamp +
                       ", frame_id=" + _frame_id + "]";
            }

            public override string ToYAMLString()
            {
                return "{\"seq\" : " + _seq + ", \"stamp\" : " +
                       _stamp.ToYAMLString() + ", frame_id=" + _frame_id + "}";
            }
        }
    }
}