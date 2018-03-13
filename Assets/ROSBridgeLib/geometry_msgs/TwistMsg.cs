using Tobii.Plugins;

namespace LiveFeedScreen.ROSBridgeLib.geometry_msgs
{
    namespace geometry_msgs
    {
        public class TwistMsg : ROSBridgeMsg
        {
            private readonly Vector3Msg _angular;
            private readonly Vector3Msg _linear;

            public TwistMsg(JSONNode msg)
            {
                _linear = new Vector3Msg(msg["linear"]);
                _angular = new Vector3Msg(msg["angular"]);
            }

            public TwistMsg(Vector3Msg linear, Vector3Msg angular)
            {
                _linear = linear;
                _angular = angular;
            }

            public static string GetMessageType()
            {
                return "geometry_msgs/Twist";
            }

            public Vector3Msg GetLinear()
            {
                return _linear;
            }

            public Vector3Msg GetAngular()
            {
                return _angular;
            }

            public override string ToString()
            {
                return "Twist [linear=" + _linear + ",  angular=" + _angular +
                       "]";
            }

            public override string ToYAMLString()
            {
                return "{\"linear\" : " + _linear.ToYAMLString() +
                       ", \"angular\" : " + _angular.ToYAMLString() + "}";
            }
        }
    }
}