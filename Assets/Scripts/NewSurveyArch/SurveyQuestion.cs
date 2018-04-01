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
        [SerializeField] public string offered_answer;

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
                    text: jsonToken["question_text"].ToString().Replace("`","'"),
                    type: jsonToken["type"].ToString().Replace("`", "'"),
                    offeredAnswerList: SeparatePipeInString(
                        jsonToken["offered_answer_text"].ToString().Replace("`", "'")),
                    offeredAnswerId: jsonToken["offered_answer_id"].ToString().Replace("`", "'"),
                    questionId: jsonToken["question_id"].ToString().Replace("`", "'"));
            return q;
        }
    }
}

