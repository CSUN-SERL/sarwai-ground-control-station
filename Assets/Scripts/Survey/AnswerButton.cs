using TransparencyIrisToOperator;
using UnityEngine;
using UnityEngine.UI;

namespace Survey
{
    /// <inheritdoc />
    /// <summary>
    ///     Responsible for sending SurveyManager the answer to question.
    /// </summary>
    /// <remarks>
    ///     Last Edited on 12/27/2017
    ///     Last Edited by Anton.
    /// </remarks>
    public class AnswerButton : MonoBehaviour
    {

        private int _behaviorHistory;
        private Button _button;

        /// <summary>
        ///     -1 = buttons calls <see cref="TransparencyBrief.PreviousQuestion()" />.
        ///     0 = buttons is inactive.
        ///     +1 = buttons calls <see cref="TransparencyBrief.PreviousQuestion()" />.
        /// </summary>
        public int BehaviorOfButton;
        
        

        /// <summary>
        ///     Sets up referene to Survey Manager and establishes OnButtonDown event.
        /// </summary>
        internal void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnMouseDown);
            _button.gameObject.SetActive(false);
        }


        /// <summary>
        ///     Answers the question.
        /// </summary>
        private void OnMouseDown()
        {
            Debug.Log(BehaviorOfButton);
            EventManager.OnChangeQuestion(BehaviorOfButton);
        }

        /// <summary>
        ///     Enables the button's function and visibility
        /// </summary>
        public void Enable()
        {
            BehaviorOfButton = _behaviorHistory;
            //_button.interactable = true;
            _button.gameObject.SetActive(true);
        }

        /// <summary>
        ///     Disables the button's function and visibility
        /// </summary>
        public void Disable()
        {
            _behaviorHistory = BehaviorOfButton;
            BehaviorOfButton = 0;
            //_button.interactable = false;
            _button.gameObject.SetActive(false);
        }
    }
}