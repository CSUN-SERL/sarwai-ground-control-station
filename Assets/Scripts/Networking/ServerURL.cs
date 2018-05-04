using System.Net;

namespace Networking
{
    public class ServerURL
    {

        public ServerURL(IPEndPoint socketEndpoint)
        {
            _socketEndpoint = socketEndpoint;
            _liveFeedEndPoint = new IPEndPoint(_socketEndpoint.Address, 8080);
        }

        private static IPEndPoint _socketEndpoint;
        private static IPEndPoint _liveFeedEndPoint;

        // Extensions
        public static string ROSBRIDGE_URL = "ws:ubuntu@192.168.1.43";
        public static int ROSBRIDGE_PORT = 9090;

        public static string MISSED_DETECTION_TOPIC = "/coffee";

        // Extension routes.
        private static readonly string GCS_ROUTE = "gcs";
        private static readonly string SURVEY_ROUTE = "survey";
        private static readonly string SOCKET_IO_ROUTE = "socket.io";
        private static readonly string INSERT_PARTICIPANT_ROUTE = "make-new-participant";
        private static readonly string RETRIEVE_SURVEY_QUESTIONS_ROUTE = "retrieve-survey-questions";
        private static readonly string RETRIEVE_QUERY_IMAGE_ROUTE = "retrieve-query-images";
        private static readonly string RETRIEVE_QUERY_FOR_TRANSPARENCY_ROUTE = "retrieve-query-images";

        private static readonly string RETRIEVE_TRANSPARENCY_BRIEF_ROUTE = "retrieve-transparency-brief";

        private static readonly string PHYSIOLOGICAL_DATA_ROUTE =
            "insert-physiological-data";

        private static readonly string LOAD_MISSION_ROUTE = "load-mission";
        private static readonly string SQL_TEMP_ROUTE = "sql-temp";
        private static readonly string INSERT_ROUTE = "insert";

        private static readonly string UPLOAD_LVL_OF_AUTONOMY_ROUTE = "upload-level-of-autonomy";

        private static readonly string QUERY_TRAPSPARENCY_ROUTE = "upload-level-of-autonomy";


        public static string UPDATE_LVL_OF_AUTONOMY
        {
            get { return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port, SURVEY_ROUTE, UPLOAD_LVL_OF_AUTONOMY_ROUTE); }
        }

        public static string JSON_TEST
        {
            get { return string.Format("http://{0}:{1}/{2}", _socketEndpoint.Address, _socketEndpoint.Port, UPLOAD_LVL_OF_AUTONOMY_ROUTE); }
        }

        // Accessors for other objects.
        public static string SOCKET_IO
        {
            get { return string.Format("http://{0}:{1}/{2}", _socketEndpoint.Address, _socketEndpoint.Port, SOCKET_IO_ROUTE); }
        }

        public static string INSERT_PARTICIPANT
        {
            get
            {
                return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port, GCS_ROUTE,
                    INSERT_PARTICIPANT_ROUTE);
            }
        }

        public static string RETRIEVE_SURVEY
        {
            get
            {
                return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port, SURVEY_ROUTE,
                    RETRIEVE_SURVEY_QUESTIONS_ROUTE);
            }
        }

        public static string RETRIEVE_IMAGE
        {
            get
            {
                return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port, SURVEY_ROUTE,
                    RETRIEVE_QUERY_IMAGE_ROUTE);
            }
        }

        public static string RETRIEVE_QUERY_FOR_TRANSPARENCY
        {
            get
            {
                return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port, QUERY_TRAPSPARENCY_ROUTE,
                    RETRIEVE_QUERY_FOR_TRANSPARENCY_ROUTE);
            }
        }

        public static string RETRIEVE_TRANSPARENCY_BRIEF
        {
            get
            {
                return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port, GCS_ROUTE,
                    RETRIEVE_TRANSPARENCY_BRIEF_ROUTE);
            }
        }

        public static string UPLOAD_PHYSIOLOGICAL_DATA
        {
            get
            {
                return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port, GCS_ROUTE,
                    PHYSIOLOGICAL_DATA_ROUTE);
            }
        }

        public static string LOAD_MISSION
        {
            get
            {
                return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port, GCS_ROUTE,
                    LOAD_MISSION_ROUTE);
            }
        }

        public static string INSERT
        {
            get
            {
                return string.Format("http://{0}:{1}/{2}/{3}", _socketEndpoint.Address, _socketEndpoint.Port,
                    SQL_TEMP_ROUTE, INSERT_ROUTE);
            }
        }

        public static string GetRobotLiveStream(int robot_id)
        {
            return string.Format(
                "http://{0}:{1}/snapshot?topic=/robot{2}/camera/rgb/image_boxed",
                _socketEndpoint.Address, _liveFeedEndPoint.Port, robot_id);

        }

        public static string DownloadMediaUrl(string fileName)
        {
            return string.Format("http://{0}:{1}/file/download/{2}", _socketEndpoint.Address, _socketEndpoint.Port,
                fileName);
        }
    }
}