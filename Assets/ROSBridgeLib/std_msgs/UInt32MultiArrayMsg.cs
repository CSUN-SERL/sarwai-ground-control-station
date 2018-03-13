using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class UInt32MultiArrayMsg : ROSBridgeMsg
        {
            private uint[] _data;
            private MultiArrayLayoutMsg _layout;

            public UInt32MultiArrayMsg(JSONNode msg)
            {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new uint[msg["data"].Count];
                for (var i = 0; i < _data.Length; i++)
                    _data[i] = uint.Parse(msg["data"][i]);
            }

            public void Int32MultiArrayMsg(MultiArrayLayoutMsg layout,
                uint[] data)
            {
                _layout = layout;
                _data = data;
            }

            public static string GetMessageType()
            {
                return "std_msgs/UInt32MultiArray";
            }

            public uint[] GetData()
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
                return "UInt32MultiArray [layout=" + _layout + ", data=" +
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