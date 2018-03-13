using System;

namespace Mission.Display.Queries
{
    public interface IAnswerButton<T>
    {
        T GetAnswer();
        void Display(object sender, EventArgs e);
    }
}