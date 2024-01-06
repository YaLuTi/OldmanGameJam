using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float xSpeed, ySpeed;
    [SerializeField]
    float mouseSensitivity;

    float cameraVerticalRotation;
    public UnityEvent<float, float> swingHead = new UnityEvent<float, float>();
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity;
        if(Mathf.Abs(y) > 0.3f) swingHead.Invoke(x, y);

        cameraVerticalRotation -= y;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        this.transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * x);
    }
}
