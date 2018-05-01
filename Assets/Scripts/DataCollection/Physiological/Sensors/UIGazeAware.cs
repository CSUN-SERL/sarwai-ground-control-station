using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGazeAware : Assets.Tobii.Framework.GazeAware
{
    BoxCollider gazeScope;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<BoxCollider>();
        gazeScope = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {

        var size = gameObject.GetComponent<RectTransform>().rect.size;
        gazeScope.size = new Vector3(size.x, size.y, 0F);
    }
}
