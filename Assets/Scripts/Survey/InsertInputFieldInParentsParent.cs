using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survey
{
    /// <inheritdoc cref="AbstractVideoControlToggle" />
    /// <seealso cref="PauseButton" />
    /// <summary>
    ///     Responsible for accessing manual control operation for robots.
    /// </summary>
    /// TODO: Implement actual controls, and not just "demo" controls
    public class InsertInputFieldInParentsParent : MonoBehaviour
    {
        public Toggle _button;


        private Transform _referenceToHorizontalGroup;

        /// <summary>
        ///     Keeps reference of other manual control operations.
        /// </summary>
        private List<InsertInputFieldInParentsParent> _siblings;

        public InputField InputFieldPrefab;


        public bool SpawnInsertFieldOnTrue;
        private GameObject SpawnofInputField;

        /// <summary>
        ///     Reference to pause button, so that video can be paused when Active().
        /// </summary>
        public void Start()
        {
            _button.onValueChanged.AddListener(value =>
            {
                if (value) Activate();
                else DeActivate();
            });
            _referenceToHorizontalGroup = gameObject.transform.parent.parent;
            //Debug.Log(_referenceToHorizontalGroup.name + "is horizontalGroup");
            var childrenCount = gameObject.transform.parent.childCount;
            _siblings = new List<InsertInputFieldInParentsParent>();
            for (var i = 0; i < childrenCount; ++i)
                gameObject.transform.parent.GetChild(i)
                    .GetComponent<InsertInputFieldInParentsParent>();
        }


        /// <inheritdoc cref="AbstractVideoControlToggle.Activate()" />
        /// <summary>
        ///     Activates Manual Control, deactivate all other instances of Manual Control.
        /// </summary>
        /// TODO: keep livefeed active, (do when livefeed is incorporated)
        public void Activate()
        {
            if (SpawnInsertFieldOnTrue)
            {
                SpawnofInputField = Instantiate(InputFieldPrefab.gameObject);
                SpawnofInputField.transform.SetParent(
                    _referenceToHorizontalGroup);
            }
        }

        /// <inheritdoc cref="AbstractVideoControlToggle.DeActivate()" />
        /// <summary>
        ///     DeActivtes Manual Control, resumes play.
        /// </summary>
        /// TODO: keep livefeed active, perhaps go back to where video was previously, when manual control started.
        public void DeActivate()
        {
            if (SpawnInsertFieldOnTrue)
                Destroy(SpawnofInputField);
            //_pause.DeActivate();
        }
    }
}