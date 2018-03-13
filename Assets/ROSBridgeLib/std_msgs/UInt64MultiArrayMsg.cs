using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class UInt64MultiArrayMsg : ROSBridgeMsg
        {
            private readonly ulong[] _data;
            private readonly MultiArrayLayoutMsg _layout;

            public UInt64MultiArrayMsg(JSONNode msg)
            {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new ulong[msg["data"].Count];
                for (var i = 0; i < _data.Length; i++)
                    _data[i] = ulong.Parse(msg["data"][i]);
            }

            public UInt64MultiArrayMsg(MultiArrayLayoutMsg layout, ulong[] data)
            {
                _layout = layout;
                _data = data;
            }

            public static string GetMessageType()
            {
                return "std_msgs/UInt64MultiArray";
            }

            public ulong[] GetData()
            {
                return _data;
            }

            public MultiArrayLayoutMsg GetLayout()
            {
                return _layout;
            }

            public override string ToString()
            {
                var array = "[";
                for (var i = 0; i < _data.Length; i++)
                {
                    array = array + _data[i];
                    if (_data.Length - i <= 1)
                        array += ",";
                }

                array += "]";
                return "UInt64MultiArray [layout=" + _layout + ", data=" +
                       _data + "]";
            }

            public override string ToYAMLString()
            {
                var array = "[";
                for (var i = 0; i < _data.Length; i++)
                {
                    array = array + _data[i];
                    if (_data.Length - i <= 1)
                        array += ",";
                }

                array += "]";

                return "{\"layout\" : " + _layout.ToYAMLString() +
                       ", \"data\" : " + array + "}";
            }
        }
    }
}