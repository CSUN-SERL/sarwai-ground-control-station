using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace NewTransparencyArch
{
    [Serializable]
    public class Question
    {
        [SerializeField] public List<string> offered_answer_text;
        [SerializeField] public string question_text;
        [SerializeField] public string type;
        

        public List<int> SelectedAnswerList { get; set; }
        public float ArrivalTime { get; set; }
        public float AnswerTime { get; set; }
        public float DepartureTime { get; set; }
        public List<float> ViewTimesList { get; set; }

        public static Question Init(string text, string type,
            List<string> offeredAnswerList)
        {
            var temp = new Question
            {
                question_text = text,
                type = type,
                offered_answer_text = offeredAnswerList
            };
            return temp;
        }

        protected static List<string> SeparatePipeInString(string pipestuff)
        {
            return Regex.Split(pipestuff, @"\|").ToList();
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
            var q = new Notification();
            JsonUtility.FromJsonOverwrite(jsonString, q);
            return q;
        }
    }

    [Serializable]
    public class Query : Notification
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
            var q = new Query();
            JsonUtility.FromJsonOverwrite(jsonString, q);
            return q;
        }
    }
}