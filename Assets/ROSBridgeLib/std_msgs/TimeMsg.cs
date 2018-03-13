﻿using Tobii.Plugins;

/**
 * Define a time message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.1
 */

namespace LiveFeedScreen.ROSBridgeLib.std_msgs
{
    namespace std_msgs
    {
        public class TimeMsg : ROSBridgeMsg
        {
            private readonly int _secs;
            private readonly int _nsecs;

            public TimeMsg(JSONNode msg)
            {
                _secs = int.Parse(msg["secs"]);
                _nsecs = int.Parse(msg["nsecs"]);
            }

            public TimeMsg(int secs, int nsecs)
            {
                _secs = secs;
                _nsecs = nsecs;
            }

            public static string GetMessageType()
            {
                return "std_msgs/Time";
            }

            public int GetSecs()
            {
                return _secs;
            }

            public int GetNsecs()
            {
                return _nsecs;
            }

            public override string ToString()
            {
                return "Time [secs=" + _secs + ",  nsecs=" + _nsecs + "]";
            }

            public override string ToYAMLString()
            {
                return "{\"data\" : {\"secs\" : " + _secs + ", \"nsecs\" : " +
                       _nsecs + "}}";
            }
        }
    }
}