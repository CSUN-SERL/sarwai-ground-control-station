﻿using Tobii.Plugins;

/**
 * Define a turtle pose message. This has been hand-crafted from the corresponding
 * turtle message file.
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 */

namespace LiveFeedScreen.ROSBridgeLib.turtlesim
{
    namespace turtlesim
    {
        public class PoseMsg : ROSBridgeMsg
        {
            private readonly float _x;
            private readonly float _y;
            private readonly float _theta;
            private readonly float _linear_velocity;
            private readonly float _angular_velocity;

            public PoseMsg(JSONNode msg)
            {
                _x = float.Parse(msg["x"]);
                _y = float.Parse(msg["y"]);
                _theta = float.Parse(msg["theta"]);
                _linear_velocity = float.Parse(msg["linear_velocity"]);
                _angular_velocity = float.Parse(msg["angular_velocity"]);
            }

            public PoseMsg(float x, float y, float theta, float linear_velocity,
                float angular_velocity)
            {
                _x = x;
                _y = y;
                _theta = theta;
                _linear_velocity = linear_velocity;
                _angular_velocity = angular_velocity;
            }

            public static string getMessageType()
            {
                return "turtlesim/Pose";
            }

            public float GetX()
            {
                return _x;
            }

            public float GetY()
            {
                return _y;
            }

            public float GetTheta()
            {
                return _theta;
            }

            public float GetLinear_Velocity()
            {
                return _linear_velocity;
            }

            public float GetAngular_Velocity()
            {
                return _angular_velocity;
            }

            public override string ToString()
            {
                return "turtlesim/Pose [x=" + _x + ",  y=" + _y + ", theta=" +
                       _theta +
                       ", linear_velocity=" + _linear_velocity +
                       ", angular_velocity=" + _angular_velocity + "]";
            }


            public override string ToYAMLString()
            {
                return "{\"x\": " + _x + ", \"y\": " + _y + ", \"theta\": " +
                       _theta +
                       ", \"linear_velocity\": " + _linear_velocity +
                       ", \"angular_velocity\": " + _angular_velocity + "}";
            }
        }
    }
}