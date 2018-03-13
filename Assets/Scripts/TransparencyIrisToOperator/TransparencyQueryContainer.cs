using System.Collections;
using System.Collections.Generic;
using Mission;
using UnityEngine;

namespace Mission
{
    public class TransparencyQueryContainer : QueryContainer
    {

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        }

        public override void InsertQuery()
        {
            Debug.Log("Child Insert Query.");   
        }
    }
}