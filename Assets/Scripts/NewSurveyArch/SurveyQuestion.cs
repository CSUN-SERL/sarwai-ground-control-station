using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace NewSurveyArch
{
    /// <summary>
    ///     Holds values that are assocaited with a survey question for ease of use.
    /// </summary>
    [Serializable]
    public class SurveyQuestion : Question
    {
        [SerializeField] public string offered_answer;
        [SerializeField] public string offered_answer_id;
        [SerializeField] public string question_id;

        /// <summary>
        ///     A way to initilize the question information and force all relevant information to be know prior to using this
        ///     object.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="offeredAnswerList"></param>
        /// <param name="offeredAnswerId"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
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


        /// <summary>
        ///     Converts a json object to an Init call.
        /// </summary>
        /// <param name="jsonToken"></param>
        /// <returns></returns>
        public static SurveyQuestion CreateFromJson(JToken jsonToken)
        {
            var q = Init(
                jsonToken["question_text"].ToString().Replace("`", "'"),
                jsonToken["type"].ToString().Replace("`", "'"),
                SeparatePipeInString(
                    jsonToken["offered_answer_text"].ToString()
                        .Replace("`", "'")),
                jsonToken["offered_answer_id"].ToString().Replace("`", "'"),
                jsonToken["question_id"].ToString().Replace("`", "'"));
            return q;
        }
    }
}