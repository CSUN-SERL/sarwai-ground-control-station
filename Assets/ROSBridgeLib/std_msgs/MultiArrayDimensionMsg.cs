using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class MultiArrayDimensionMsg : ROSBridgeMsg
        {
            private readonly string _label;
            private readonly uint _size;
            private readonly uint _stride;

            public MultiArrayDimensionMsg(JSONNode msg)
            {
                _label = msg["label"];
                _size = uint.Parse(msg["size"]);
                _stride = uint.Parse(msg["stride"]);
            }

            public MultiArrayDimensionMsg(string label, uint size, uint stride)
            {
                _label = label;
                _size = size;
                _stride = stride;
            }

            public static string GetMessageType()
            {
                return "std_msgs/MultiArrayDimension";
            }

            public string GetLabel()
            {
                return _label;
            }

            public uint GetSize()
            {
                return _size;
            }

            public uint GetStride()
            {
                return _stride;
            }

            public override string ToString()
            {
                return "MultiArrayDimension [label=" + _label + ", size=" +
                       _size + ", stride = " + _stride + "]";
            }

            public override string ToYAMLString()
            {
                return "{\"label\" : " + _label + ",\"size\" :" + _size +
                       ",\"stride\" :" + _stride + "}";
            }
        }
    }
}