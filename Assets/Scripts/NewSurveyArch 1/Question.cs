using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewSurveyArch
{
    [Serializable]
    public abstract class Question : ScriptableObject
    {
        [SerializeField] private List<string> _offeredAnswerList;
        [SerializeField] private string _text;
        [SerializeField] private string _type;

        public string Text
        {
            get { return _text; }
        }

        public string Type
        {
            get { return _type; }
        }

        public List<string> OfferedAnswerList
        {
            get { return _offeredAnswerList; }
        }

        public List<int> SelectedAnswerList { get; set; }
        public float ArrivalTime { get; set; }
        public float AnswerTime { get; set; }
        public float DepartureTime { get; set; }
        public List<float> ViewTimesList { get; set; }

        public static Question Init(string text, string type,
            List<string> offeredAnswerList)
        {
            var temp = CreateInstance<Question>();
            temp._text = text;
            temp._type = type;
            temp._offeredAnswerList = offeredAnswerList;
            return temp;
        }

        public static Question CreateFromJson(string jsonString)
        {
            var q = CreateInstance<Question>();
            JsonUtility.FromJsonOverwrite(jsonString, q);
            return q;
        }
    }


    [Serializable]
    public class SurveyQuestion : Question
    {
        [SerializeField] private string _offeredAnswerId;
        [SerializeField] private string _questionId;

        public string OfferedAnswerId
        {
            get { return _offeredAnswerId; }
        }

        public string QuestionId
        {
            get { return _questionId; }
        }

        public static  SurveyQuestion Init(
            string text,
            string type,
            List<string> offeredAnswerList,
            string offeredAnswerId,
            string questionId)

        {
            var temp =
                Question.Init(text, type, offeredAnswerList) as SurveyQuestion;
            temp._offeredAnswerId = offeredAnswerId;
            temp._questionId = questionId;
            return temp;
        }

        public new static SurveyQuestion CreateFromJson(string jsonString)
        {
            var q = CreateInstance<SurveyQuestion>();
            JsonUtility.FromJsonOverwrite(jsonString, q);
            return q;
        }
    }


    [Serializable]
    public class Notification : Question
    {
        [SerializeField] private string _title;

        public string Title
        {
            get { return _title; }
        }

        public static Notification Init(
            string text,
            string type,
            List<string> offeredAnswerList,
            string title)
        {
            var temp = Question.Init(text, type,
                offeredAnswerList) as Notification;
            temp._title = title;
            return temp;
        }

        public new static Notification CreateFromJson(string jsonString)
        {
            var q = CreateInstance<Notification>();
            JsonUtility.FromJsonOverwrite(jsonString, q);
            return q;
        }
    }

    [Serializable]
    public abstract class Query : Notification
    {
        [SerializeField] private int _queryId;
        [SerializeField] private int _robotId;

        public int QueryId
        {
            get { return _queryId; }
        }

        public int RobotId
        {
            get { return _robotId; }
        }

        public static  Query Init(
            string text,
            string type,
            List<string> offeredAnswerList,
            int queryId,
            int robotId)
        {
            var temp = Notification.Init(text, type,
                offeredAnswerList) as Query;
            temp._queryId = queryId;
            temp._robotId = robotId;
            return  temp;
        }
        public new static Query CreateFromJson(string jsonString)
        {
            var q = CreateInstance<Query>();
            JsonUtility.FromJsonOverwrite(jsonString, q);
            return q;
        }
    }
}