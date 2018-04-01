using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewSurveyArch
{
    /// <inheritdoc cref="AbstractVideoControlToggle" />
    /// <seealso cref="PauseButton" />
    /// <summary>
    ///     Responsible for accessing manual control operation for robots.
    /// </summary>
    /// TODO: Implement actual controls, and not just "demo" controls
    public class InsertInputFieldInParentsParent : MonoBehaviour
    {
        private Transform _referenceToHorizontalGroup;
        
        private List<InsertInputFieldInParentsParent> _siblings;


        public bool SpawnInsertFieldOnTrue = false;
        private GameObject SpawnofInputField;
        
        public void Start()
        {
            gameObject.GetComponent<Toggle>().onValueChanged.AddListener(value =>
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
        
        public void Activate()
        {
            if (SpawnInsertFieldOnTrue)
            {
                SpawnofInputField = Instantiate(Resources.Load<GameObject>("SurveyQuestion/InputField"));
                SpawnofInputField.transform.SetParent(
                    _referenceToHorizontalGroup);
            }
        }

        public void DeActivate()
        {
            if (SpawnInsertFieldOnTrue)
                Destroy(SpawnofInputField);
            //_pause.DeActivate();
        }
    }
}