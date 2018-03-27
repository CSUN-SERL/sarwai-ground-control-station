using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewSurveyArch
{
    [Serializable]
    public class SurveyQuestion : Question
    {
        [SerializeField] public string offered_answer_id;
        [SerializeField] public string question_id;

        public static SurveyQuestion Init(
            string text,
            string type,
            List<string> offeredAnswerList,
            string offeredAnswerId,
            string questionId)

        {
            var temp = new SurveyQuestion
            {
                question_text = text,
                type = type,
                offered_answer_text = offeredAnswerList,
                offered_answer_id = offeredAnswerId,
                question_id = questionId
            };
            return temp;
        }



        public new static SurveyQuestion CreateFromJson(JToken jsonToken)
        {
            var q = SurveyQuestion.Init(
                    text: jsonToken["question_text"].ToString(),
                    type: jsonToken["type"].ToString(),
                    offeredAnswerList: SeparatePipeInString(
                        jsonToken["offered_answer_text"].ToString()),
                    offeredAnswerId: jsonToken["offered_answer_id"].ToString(),
                    questionId: jsonToken["question_id"].ToString());
            return q;
        }
    }
}

