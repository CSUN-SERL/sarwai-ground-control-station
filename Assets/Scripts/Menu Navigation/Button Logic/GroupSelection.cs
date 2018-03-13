using UnityEngine;
using UnityEngine.UI;

namespace Menu_Navigation.Button_Logic
{
    /// <summary>
    ///     Creates a new paricipant in the selected group.
    /// </summary>
    public class GroupSelection : MonoBehaviour
    {
        public static InputField InputField;

        public GameObject ProctorInputField;

        private void Awake()
        {
            InputField = ProctorInputField.GetComponent<InputField>();
            InputField.onValueChanged.AddListener(delegate
            {
                ValidateInput();
            });
        }

        private void ValidateInput()
        {
            foreach (var go in GameObject.FindGameObjectsWithTag("MenuButton"))
                go.GetComponent<Button>().interactable =
                    InputField.text.Length > 0;
        }
    }
}