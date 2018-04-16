namespace Networking
{
    public class ServerURL
    {
		// Main URLs and Ports for servers.
		//private const string URL =
		//    "http://ec2-52-24-126-225.us-west-2.compute.amazonaws.com";

		public const string URL = "http://192.168.1.11"; // "http://52.24.126.225";

		//private const int PORT = 81;
		public const int PORT = 8000;

        public const string ROSBRIDGE_URL = "ws:ubuntu@192.168.1.43";
        public const int ROSBRIDGE_PORT = 9090;

        public const string MISSED_DETECTION_TOPIC = "/coffee";


        private const string STATION_1_URL = "192.168.1.161";
        private const int STATION_1_PORT = 8080;

        private const string STATION_4_URL = "192.168.1.43";
        private const int STATION_4_PORT = 8080;

        // Extension routes.
        private const string GCS_ROUTE = "gcs";
        private const string SURVEY_ROUTE = "survey";
        private const string SOCKET_IO_ROUTE = "socket.io";
        private const string INSERT_PARTICIPANT_ROUTE = "make-new-participant";
        private const string RETRIEVE_SURVEY_QUESTIONS_ROUTE = "retrieve-survey-questions";
        private const string RETRIEVE_QUERY_IMAGE_ROUTE = "retrieve-query-images";
        private const string RETRIEVE_QUERY_FOR_TRANSPARENCY_ROUTE = "retrieve-query-images";
        private const string RETRIEVE_TRANSPARENCY_BRIEF_ROUTE = "retrieve-transparency-brief";
        //iris\retrieve-query-images
        //question_id


        private const string PHYSIOLOGICAL_DATA_ROUTE =
            "insert-physiological-data";

        private const string LOAD_MISSION_ROUTE = "load-mission";
        private const string SQL_TEMP_ROUTE = "sql-temp";
        private const string INSERT_ROUTE = "insert";

        private const string UPLOAD_LVL_OF_AUTONOMY_ROUTE = "upload-level-of-autonomy";

        private const string QUERY_TRAPSPARENCY_ROUTE = "upload-level-of-autonomy";



        // Socket Event Names
        public const string QUERY_RECEIVED = "gcs-query-received";
        public const string NOTIFICATION_RECEIVED = "gcs-notification-received";
        public const string SEND_ANSWER_QUERY = "gcs-query-answers";

        public const string TOGGLE_MANUAL_CONTROL = "cm-toggle-manual-control";

        public static string UPDATE_LVL_OF_AUTONOMY
        {
            get
            {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT, SURVEY_ROUTE, UPLOAD_LVL_OF_AUTONOMY_ROUTE);
            }
        }

        public static string JSON_TEST
        {
            get
            {
                return string.Format("{0}:{1}/{2}", URL, PORT, UPLOAD_LVL_OF_AUTONOMY_ROUTE);
            }
        }

        // Accessors for other objects.
        public static string SOCKET_IO
        {
            get
            {
                return string.Format("{0}:{1}/{2}", URL, PORT, SOCKET_IO_ROUTE);
            }
        }

        public static string INSERT_PARTICIPANT
        {
            get
            {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT, GCS_ROUTE,
                    INSERT_PARTICIPANT_ROUTE);
            }
        }

        public static string RETRIEVE_SURVEY
        {
            get
            {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT, SURVEY_ROUTE,
                    RETRIEVE_SURVEY_QUESTIONS_ROUTE);
            }
        }

        public static string RETRIEVE_IMAGE
        {
            get
            {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT, SURVEY_ROUTE,
                    RETRIEVE_QUERY_IMAGE_ROUTE);
            }
        }

        public static string RETRIEVE_QUERY_FOR_TRANSPARENCY
        {
            get
            {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT, QUERY_TRAPSPARENCY_ROUTE,
                    RETRIEVE_QUERY_FOR_TRANSPARENCY_ROUTE);
            }
        }

        public static string RETRIEVE_TRANSPARENCY_BRIEF {
            get {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT, GCS_ROUTE,
                    RETRIEVE_TRANSPARENCY_BRIEF_ROUTE);
            }
        }

        public static string UPLOAD_PHYSIOLOGICAL_DATA
        {
            get
            {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT, GCS_ROUTE,
                    PHYSIOLOGICAL_DATA_ROUTE);
            }
        }

        public static string LOAD_MISSION
        {
            get
            {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT, GCS_ROUTE,
                    LOAD_MISSION_ROUTE);
            }
        }

        public static string INSERT
        {
            get
            {
                return string.Format("{0}:{1}/{2}/{3}", URL, PORT,
                    SQL_TEMP_ROUTE, INSERT_ROUTE);
            }
        }

        public static string GetRobotLiveStream(int robot_id)
        {
            if (robot_id == 1 || robot_id == 2)
                return string.Format(
                    "http://{0}:{1}/snapshot?topic=/robot{2}/camera/rgb/image_boxed",
                    STATION_4_URL, STATION_1_PORT, robot_id);
            if (robot_id == 3 || robot_id == 4)
                return string.Format(
                    "http://{0}:{1}/snapshot?topic=/robot{2}/camera/rgb/image_boxed",
                    STATION_4_URL, STATION_4_PORT, robot_id);
            return null;
        }

        public static string DownloadMediaUrl(string fileName)
        {
            return string.Format("{0}:{1}/file/download/{2}", URL, PORT,
                fileName);
        }


    }
}