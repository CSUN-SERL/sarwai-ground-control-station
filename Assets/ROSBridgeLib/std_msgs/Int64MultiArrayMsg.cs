using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class Int64MultiArrayMsg : ROSBridgeMsg
        {
            private long[] _data;
            private MultiArrayLayoutMsg _layout;

            public Int64MultiArrayMsg(JSONNode msg)
            {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new long[msg["data"].Count];
                for (var i = 0; i < _data.Length; i++)
                    _data[i] = long.Parse(msg["data"][i]);
            }

            public void UInt64MultiArrayMsg(MultiArrayLayoutMsg layout,
                long[] data)
            {
                _layout = layout;
                _data = data;
            }

            public static string GetMessageType()
            {
                return "std_msgs/Int64MultiArray";
            }

            public long[] GetData()
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
                return "Int64MultiArray [layout=" + _layout + ", data=" +
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