using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class UInt16MultiArrayMsg : ROSBridgeMsg
        {
            private readonly ushort[] _data;
            private readonly MultiArrayLayoutMsg _layout;

            public UInt16MultiArrayMsg(JSONNode msg)
            {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new ushort[msg["data"].Count];
                for (var i = 0; i < _data.Length; i++)
                    _data[i] = ushort.Parse(msg["data"][i]);
            }

            public UInt16MultiArrayMsg(MultiArrayLayoutMsg layout,
                ushort[] data)
            {
                _layout = layout;
                _data = data;
            }

            public static string GetMessageType()
            {
                return "std_msgs/UInt16MultiArray";
            }

            public ushort[] GetData()
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
                return "UInt16MultiArray [layout=" + _layout + ", data=" +
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