using UnityEngine;

public class WelcomeStartup : MonoBehaviour
{
    public Camera camera1;

    public Camera camera2;

    public Canvas canvas1;

    public Canvas canvas2;

    // Use this for initialization
    private void Start()
    {
        camera1.enabled = true;
        camera2.enabled = false;
        canvas1.enabled = true;
        canvas2.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}