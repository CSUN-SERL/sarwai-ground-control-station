using System;
using System.Collections.Generic;
using UnityEngine;

namespace Survey
{
    [Serializable]
    public class QuestionDetails : ScriptableObject
    {
        public string OfferedAnswerId;
        public List<string> OfferedAnswerList;
        public string QuestionId;
        public string QuestionString;
        public string QuestionType;
        public string SelectedAnswer;
        public string SurveyId;
    }
}