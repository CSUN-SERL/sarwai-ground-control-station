using System;
using Mission;
using UnityEngine;

namespace NewSurveyArch
{
    public class ButtonNameEventArgs : System.EventArgs
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }

    public static class ButtonEventManager
    {
        public static event EventHandler<StringEventArgs> Load;
        //public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;

        public static void OnLoad(string name)
        {
            var handler = Load;
            if (handler != null) handler(null, new StringEventArgs {StringArgs = name});
        }

        public static event EventHandler<StringEventArgs> End;
        //public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;

        public static void OnEnd(string name)
        {
            var handler = End;
            if (handler != null) handler(null, new StringEventArgs { StringArgs = name });
        }

        public static event EventHandler<EventArgs> BeginQuestion;
        public static void OnBeginQuestion()
        {
            var handler = BeginQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        public static event EventHandler<EventArgs> NextQuestion;
        public static void OnNextQuestion()
        {
            var handler = NextQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        public static event EventHandler<EventArgs> ContinueQuestion;
        public static void OnContinueQuestion()
        {
            var handler = ContinueQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        public static event EventHandler<EventArgs> QuestionNotComplete;
        public static void OnQuestionNotComplete()
        {
            var handler = QuestionNotComplete;
            if (handler != null) handler(null, new EventArgs());
        }


    }
}
