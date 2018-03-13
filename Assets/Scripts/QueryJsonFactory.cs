using System.Collections.Generic;
using Mission.Queries.QueryTypes.Audio;
using Mission.Queries.QueryTypes.Visual;
using Newtonsoft.Json;
using UnityEngine;

public class QueryJsonFactory : MonoBehaviour
{
    public static string MakeQueryJson(string type)
    {
        switch (type)
        {
            case VisualDetectionQuery.QueryType:
                return JsonConvert.SerializeObject(new QueryJson
                {
                    type = VisualDetectionQuery.QueryType,
                    data = new Dictionary<string, string>
                    {
                        {"query_id", "100"},
                        {"robot_id", "0"},
                        {"confidence", "0.80"},
                        {"file_path", "test.png"}
                    }
                });
            case AudioDetectionQuery.QueryType:
                return JsonConvert.SerializeObject(new QueryJson
                {
                    type = AudioDetectionQuery.QueryType,
                    data = new Dictionary<string, string>
                    {
                        {"query_id", "100"},
                        {"robot_id", "0"},
                        {"confidence", "0.80"},
                        {"file_path", "test.ogg"}
                    }
                });
            default:
                return "fuck";
        }
    }
}