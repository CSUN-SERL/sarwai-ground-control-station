using System;

namespace NewSurveyArch
{
    /// <summary>
    ///     Handles the event that coordinate the actions of a button.
    /// </summary>
    public static class ButtonEventManager
    {
        /// <summary>
        ///     Event for when the button needs to display "Begin".
        /// </summary>
        public static event EventHandler<EventArgs> BeginQuestion;

        public static void OnBeginQuestion()
        {
            var handler = BeginQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        /// <summary>
        ///     Event for when the button needs to display "Next".
        /// </summary>
        public static event EventHandler<EventArgs> NextQuestion;

        public static void OnNextQuestion()
        {
            var handler = NextQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        /// <summary>
        ///     Event for when the button needs to display "Continue".
        /// </summary>
        public static event EventHandler<EventArgs> ContinueQuestion;

        public static void OnContinueQuestion()
        {
            var handler = ContinueQuestion;
            if (handler != null) handler(null, new EventArgs());
        }

        /// <summary>
        ///     Event for when the button needs to display "Cannot go to next question until current question is answered." +
        ///     "Next".
        /// </summary>
        public static event EventHandler<EventArgs> QuestionNotComplete;

        public static void OnQuestionNotComplete()
        {
            var handler = QuestionNotComplete;
            if (handler != null) handler(null, new EventArgs());
        }
    }
}