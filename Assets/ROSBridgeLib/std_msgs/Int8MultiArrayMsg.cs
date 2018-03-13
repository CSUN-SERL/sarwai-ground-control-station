using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class Int8MultiArrayMsg : ROSBridgeMsg
        {
            private readonly sbyte[] _data;
            private readonly MultiArrayLayoutMsg _layout;

            public Int8MultiArrayMsg(JSONNode msg)
            {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new sbyte[msg["data"].Count];
                for (var i = 0; i < _data.Length; i++)
                    _data[i] = sbyte.Parse(msg["data"][i]);
            }

            public Int8MultiArrayMsg(MultiArrayLayoutMsg layout, sbyte[] data)
            {
                _layout = layout;
                _data = data;
            }

            public static string GetMessageType()
            {
                return "std_msgs/Int8MultiArray";
            }

            public sbyte[] GetData()
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
                return "Int8MultiArray [layout=" + _layout + ", data=" + _data +
                       "]";
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