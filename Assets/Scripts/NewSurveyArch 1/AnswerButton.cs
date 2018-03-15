using System.Security.AccessControl;
using Mission;
using UnityEngine;
using UnityEngine.UI;

namespace NewSurveyArch
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
        private Image _button;

        private void Awake()
        {
            _button = gameObject.GetComponentInChildren<Image>();
            //hides the button image
            _button.enabled = false;

        }

        internal void OnEnable()
        {
            
            ButtonEventManager.Load += OnLoad;
            ButtonEventManager.End += OnEnd;
            ButtonEventManager.ChangeText += OnChangeText;
        }

        public void OnDisable()
        {
            ButtonEventManager.Load -= OnLoad;
            ButtonEventManager.End -= OnEnd;
            ButtonEventManager.ChangeText -= OnChangeText;
        }
        
        /// <summary>
        ///     Changes the text associated with this button if called by button's text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChangeText(object sender, ButtonNameEventArgs e)
        {
            if (GetComponentInChildren<Text>().text == e.OldName)
                GetComponentInChildren<Text>().text = e.NewName;
        }

        /// <summary>
        ///     Sets current button to active if called by button's text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoad(object sender, StringEventArgs e)
        {
            if (GetComponentInChildren<Text>().text == e.StringArgs)
                _button.enabled = true;
        }

        /// <summary>
        ///     Sets current button to inactive if called by button's text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEnd(object sender, StringEventArgs e)
        {
            if (GetComponentInChildren<Text>().text == e.StringArgs)
                _button.enabled = false;
        }
    }
}