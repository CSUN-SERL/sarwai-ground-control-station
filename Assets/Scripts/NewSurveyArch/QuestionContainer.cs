using System;
using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

namespace NewSurveyArch
{
    public abstract class QuestionContainer : MonoBehaviour
    {
        protected List<Question> QuestionList;
        /// <summary>
        ///     Displays once the listener recieves the call.
        /// </summary>
        public abstract void DisplayOn();

        /// <summary>
        ///     Stosp displaying once the listener recieves the call.
        /// </summary>
        public abstract void DisplayOff();

        protected abstract void Upload();

    }

   
    public class MessegeContainer : QuestionContainer
    {

        private SurveyQuestion _question;

        /// <summary>
        ///     Displays once the listener recieves the call.
        /// </summary>
        public override void DisplayOn()
        {
            gameObject.GetComponent<Image>().enabled = true;
        }

        /// <summary>
        ///     Stosp displaying once the listener recieves the call.
        /// </summary>
        public override void DisplayOff()
        {
            gameObject.GetComponent<Image>().enabled = false;
        }

        protected override void Upload()
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        ///     Stores question locally.
        /// </summary>
        /// <param name="question"></param>
        public void Init(SurveyQuestion question)
        {
            _question = question;
        }
    }
}
