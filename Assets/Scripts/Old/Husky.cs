// The purpose of Husky.cs is to get keyboard input from the arrow keys. 
// That input is then manipulated to be the movement of the Husky bot. 
// We can change the Husky's velocity thorugh this script to our needs.
// The data in this script is sent though to ROSBridge and is being published 
// to a topic at real-time.

using LiveFeedScreen.ROSBridgeLib;
using LiveFeedScreen.ROSBridgeLib.geometry_msgs.geometry_msgs;
using UnityEngine;
using UnityEngine.UI;

namespace LiveFeedScreen.E2SHPackage_Scripts
{
    public class Husky : MonoBehaviour
    {
        private ROSBridgeWebSocketConnection _ros;
        private string _topic = "/cmd_vel";
        private bool _useJoysticks;

        // Will create bot instances
        public Toggle Isbot1;
        public Toggle Isbot2;
        public Toggle Isbot3;
        public Toggle Isbot4;


        // The critical thing here is to define our subscribers, publishers and service response handlers.\
        private void Start()
        {
            _useJoysticks = Input.GetJoystickNames().Length > 0;

            // ros will be a node with said connection below... To our AWS server.
            //_ros = new ROSBridgeWebSocketConnection ("ws:ubuntu@13.57.99.200", 9090); 
            //local url 
            _ros = new ROSBridgeWebSocketConnection("ws:ubuntu@192.168.1.43",
                9090);

            // Gives a live connection to ROS via ROSBridge.
            _ros.Connect();
        }

        // Extremely important to disconnect from ROS. OTherwise packets continue to flow.
        private void OnApplicationQuit()
        {
            if (_ros != null)
                _ros.Disconnect();
        }

        // Update is called once per frame in Unity. We use the joystick or cursor keys to generate teleoperational commands
        // that are sent to the ROS world, which drives the robot which ...
        private void Update()
        {
            // Instantiates variables with keyboad input (Lines 44 - 62).
            float _dx, _dy;

            if (_useJoysticks)
            {
                _dx = Input.GetAxis("Joy0X");
                _dy = Input.GetAxis("Joy0Y");
            }
            else
            {
                _dx = Input.GetAxis("Horizontal");
                _dy = Input.GetAxis("Vertical");
            }

            // Multiplying _dy or _dx by a larger value, increases "speed".
            // Linear is responsibile for forward and backward movment.
            var linear = _dy * 3.0f;
            //angular is responsible for rotaion.
            var angular = -_dx * 2.0f;

            // Create a ROS Twist message from the keyboard input. This input/twist message, creates the data that will in turn move the 
            // bot on the ground.
            var msg = new TwistMsg(new Vector3Msg(linear, 0.0, 0.0),
                new Vector3Msg(0.0, 0.0, angular));

            ActiveToggle(); ///////\\\\\\\\\ Need to call func to get appropriate topic
            // name for the correct selected bot. This is for testing of 
            // the 4 bot environment!!!!!!

            // Publishes the TwistMsg values over to the /cmd_vel topic in ROS.
            _ros.Publish("/cmd_vel", msg);

            _ros.Publish(_topic,
                msg); /////////\\\\\\\\\\ this is for testing of the 4 bots 
            // environment!!!	

            _ros.Render();
        }

        private void ActiveToggle()
        {
            if (Isbot1.isOn)
                _topic = "/robot1/cmd_vel";
            if (Isbot2.isOn)
                _topic = "/robot2/cmd_vel";
            if (Isbot3.isOn)
                _topic = "/robot3/cmd_vel";
            if (Isbot4.isOn)
                _topic = "/robot4/cmd_vel";
        }
    }
}