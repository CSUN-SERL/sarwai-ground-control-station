using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Display.Queries
{
    public abstract class AnswerButton<T> : MonoBehaviour, IAnswerButton<T>
    {
        protected T Answer;
        protected Button AnswerButt;
        public Sprite ButtonSprite;

        public T GetAnswer()
        {
            return Answer;
        }

        public abstract void Display(object sender, EventArgs e);

        private void Start()
        {
            AnswerButt = gameObject.GetComponent<Button>();
        }
    }
}