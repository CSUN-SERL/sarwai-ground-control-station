using System;
using LiveFeedScreen.ROSBridgeLib.std_msgs.std_msgs;
using Tobii.Plugins;

/**
 * Define a compressed image message. Note: the image is assumed to be in Base64 format.
 * Which seems to be what is normally found in json strings. Documentation. Got to love it.
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.1
 */

namespace LiveFeedScreen.ROSBridgeLib.sensor_msgs
{
    namespace sensor_msgs
    {
        public class CompressedImageMsg : ROSBridgeMsg
        {
            private readonly byte[] _data;
            private readonly string _format;
            private readonly HeaderMsg _header;


            public CompressedImageMsg(JSONNode msg)
            {
                _format = msg["format"];
                _header = new HeaderMsg(msg["header"]);
                _data = Convert.FromBase64String(msg["data"]);
            }

            public CompressedImageMsg(HeaderMsg header, string format,
                byte[] data)
            {
                _header = header;
                _format = format;
                _data = data;
            }

            public byte[] GetImage()
            {
                return _data;
            }

            public static string GetMessageType()
            {
                return "sensor_msgs/CompressedImage";
            }

            public override string ToString()
            {
                return "Compressed Image [format=" + _format + ",  size=" +
                       _data.Length + ", Header " + _header + "]";
            }

            public override string ToYAMLString()
            {
                return "{\"format\" : " + "\"" + _format + "\", \"data\" : \"" +
                       Convert.ToBase64String(_data) + "\", \"header\" : " +
                       _header.ToYAMLString() + "}";
            }
        }
    }
}