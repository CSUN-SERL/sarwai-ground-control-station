using UnityEngine;

public class LoadingCircle : MonoBehaviour {
    private RectTransform rectComponent;
    private float rotateSpeed = 200f;

    void OnEnable() {
        rectComponent = GetComponent<RectTransform>();
    }

    private void Update() {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}