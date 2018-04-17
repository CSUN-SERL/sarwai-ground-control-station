using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewTransparencyArch
{
    /// <summary>
    ///     Creates a textbox for questions in the survey that require an explanation.
    /// </summary>TODO:This script is probably broken, needs to follow a different cleaner behavior.
    public class InsertInputFieldInParentsParent : MonoBehaviour
    {

        /// <summary>
        ///     Reference to the AnswerTypeBar inside of Multiple and Scalar prefabs.
        /// </summary>
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