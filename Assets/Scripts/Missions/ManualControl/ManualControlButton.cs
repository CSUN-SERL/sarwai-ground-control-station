using LiveFeedScreen.ROSBridgeLib;
using LiveFeedScreen.ROSBridgeLib.geometry_msgs.geometry_msgs;
using Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.ManualControl
{
    public class ManualControlButton : MonoBehaviour
    {
        public static int CurrentRobot;
        private string _aws = "ws:ubuntu@13.57.99.200";

        private float _linear, _angular;

        private ROSBridgeWebSocketConnection _ros;
        private ROSBridgeWebSocketConnection _ros1;
        private ROSBridgeWebSocketConnection _ros4;
        private readonly string _station1 = "ws:ubuntu@192.168.1.161";
        private readonly string _station4 = "ws:ubuntu@192.168.1.43";

        private string _topic = "/robot1/cmd_vel";

        private bool _useJoysticks;

        private bool flag1 = true, flag2 = true, flag3 = true, flag4 = true;

        // Will create bot instances
        public Toggle Isbot1;
        public Toggle Isbot2;
        public Toggle Isbot3;
        public Toggle Isbot4;
        

        // The critical thing here is to define our subscribers, publishers and service response handlers.
        private void Start()
        {
            _useJoysticks = Input.GetJoystickNames().Length > 0;

            // ros will be a node with said connection below... To our AWS server.
            _ros = new ROSBridgeWebSocketConnection(_station4, 9090);
            _ros1 = new ROSBridgeWebSocketConnection(_station1, 9090);
            _ros4 = new ROSBridgeWebSocketConnection(_station4, 9090);


            // Gives a live connection to ROS via ROSBridge.
            _ros.Connect();
            _ros1.Connect();
            _ros4.Connect();
        }

        // Extremely important to disconnect from ROS. OTherwise packets continue to flow.
        private void OnApplicationQuit()
        {
            if (_ros != null)
                _ros.Disconnect();
            if (_ros1 != null)
                _ros1.Disconnect();
            if (_ros4 != null)
                _ros4.Disconnect();

            if (Isbot1.isOn && flag1 == false)
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    1);
            if (Isbot2.isOn && flag2 == false)
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    1);
            if (Isbot3.isOn && flag3 == false)
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    1);
            if (Isbot4.isOn && flag4 == false)
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    1);
        }

        // Update is called once per frame in Unity. We use the joystick or cursor keys to generate teleoperational commands
        // that are sent to the ROS world, which drives the robot which ...
        private void Update()
        {
            if (_useJoysticks)
            {
                _angular = Input.GetAxis("Joy0X");
                _linear = Input.GetAxis("Joy0Y");
            }
            else
            {
                _angular = Input.GetAxis("Horizontal");
                _linear = Input.GetAxis("Vertical");
            }

            // Multiplying _dy or _dx by a larger value, increases "speed".
            // Linear is responsibile for forward and backward movment.
            _linear *= 1.0f;
            //angular is responsible for rotation.
            _angular = -_angular * 1.0f;

            // Create a ROS Twist message from the keyboard input. This input/twist message, creates the data that will in turn move the 
            // bot on the ground.
            var msg = new TwistMsg(new Vector3Msg(_linear, 0.0, 0.0),
                new Vector3Msg(0.0, 0.0, _angular));
            _topic = "";
            ActiveToggle();

            // Publishes the TwistMsg values over to the /cmd_vel topic in ROS.
            //_ros.Publish("/cmd_vel", msg);

            _ros.Publish(_topic,
                msg); /////////\\\\\\\\\\ this is for testing of the 4 bots 
            // environment!!!	


            _ros.Render();
        }

        /*
        private void OnDestroy()
        {
            if (Isbot1.isOn && flag1 == false)
                GcsSocket.Emit(GcsSocket.TOGGLE_MANUAL_CONTROL, 1);
            if (Isbot2.isOn && flag2 == false)
                GcsSocket.Emit(GcsSocket.TOGGLE_MANUAL_CONTROL, 1);
            if (Isbot3.isOn && flag3 == false)
                GcsSocket.Emit(GcsSocket.TOGGLE_MANUAL_CONTROL, 1);
            if (Isbot4.isOn && flag4 == false)
                GcsSocket.Emit(GcsSocket.TOGGLE_MANUAL_CONTROL, 1);
        }
    */

        private void ActiveToggle()
        {
            if (Isbot1.isOn)
            {
                _topic = "/robot1/cmd_vel";
                _ros = _ros1;
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    1);
                flag1 = false;
            }
            else if (Isbot2.isOn)
            {
                _topic = "/robot2/cmd_vel";
                _ros = _ros1;
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    2);
                flag2 = false;
            }
            else if (Isbot3.isOn)
            {
                _topic = "/robot3/cmd_vel";
                _ros = _ros4;
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    3);
                flag3 = false;
            }
            else if (Isbot4.isOn)
            {
                _topic = "/robot4/cmd_vel";
                _ros = _ros4;
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    4);
                flag4 = false;
            }

            // turn waypoints back on
            if (!Isbot1.isOn && flag1 == false)
            {
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    1);
                flag1 = true;
            }
            else if (!Isbot2.isOn && flag2 == false)
            {
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    2);
                flag2 = true;
            }
            else if (!Isbot3.isOn && flag3 == false)
            {
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    3);
                flag3 = true;
            }
            else if (!Isbot4.isOn && flag4 == false)
            {
                GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL,
                    4);
                flag4 = true;
            }


            //TODO 
            /*
        if (CurrentRobot == robotId)
        {
            _topic = "";
            CurrentRobot = -1;
            GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL, CurrentRobot);
            Debug.Log(robotId);
            Debug.Log(CurrentRobot);
            Debug.Log(_topic);
            Debug.Log(_ros);
            return;
        }


        // The current robot cannot be equal to robotID because of the statement above.  
        // If there is a robot selected, turn off manual control
        if (CurrentRobot <= 1)
        {
            GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL, CurrentRobot);
        }

        CurrentRobot = robotId;
        GcsSocket.Emit(ServerURL.TOGGLE_MANUAL_CONTROL, CurrentRobot);
        _topic = string.Format("/robot{0}/cmd_vel", CurrentRobot);
        _ros = _ros4;

        Debug.Log(robotId);
        Debug.Log(CurrentRobot);
        Debug.Log(_topic);
        Debug.Log(_ros);*/
        }
    }
}