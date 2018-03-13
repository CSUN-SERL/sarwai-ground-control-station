using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class UInt32Msg : ROSBridgeMsg
        {
            private readonly uint _data;

            public UInt32Msg(JSONNode msg)
            {
                _data = uint.Parse(msg["data"]);
            }

            public UInt32Msg(uint data)
            {
                _data = data;
            }

            public static string GetMessageType()
            {
                return "std_msgs/UInt32";
            }

            public uint GetData()
            {
                return _data;
            }

            public override string ToString()
            {
                return "UInt32 [data=" + _data + "]";
            }

            public override string ToYAMLString()
            {
                return "{\"data\" : " + _data + "}";
            }
        }
    }
}