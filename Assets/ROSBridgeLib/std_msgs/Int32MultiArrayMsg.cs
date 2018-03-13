using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class Int32MultiArrayMsg : ROSBridgeMsg
        {
            private readonly int[] _data;
            private readonly MultiArrayLayoutMsg _layout;

            public Int32MultiArrayMsg(JSONNode msg)
            {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new int[msg["data"].Count];
                for (var i = 0; i < _data.Length; i++)
                    _data[i] = int.Parse(msg["data"][i]);
            }

            public Int32MultiArrayMsg(MultiArrayLayoutMsg layout, int[] data)
            {
                _layout = layout;
                _data = data;
            }

            public static string getMessageType()
            {
                return "std_msgs/Int32MultiArray";
            }

            public int[] GetData()
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
                return "Int32MultiArray [layout=" + _layout + ", data=" +
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