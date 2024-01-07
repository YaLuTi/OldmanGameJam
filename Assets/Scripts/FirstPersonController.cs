using Shapes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    MoteableObject nowHighLight;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHead();
        SeeObject();

        if(Input.GetKeyDown(KeyCode.P)) SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    void MoveHead()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= y;
        if (Mathf.Abs(cameraVerticalRotation) > 15f || Mathf.Abs(y) > 0.4f) swingHead.Invoke(y, cameraVerticalRotation);
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        this.transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * x);
    }

    private void SeeObject()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MoteableObject moteableObject;
            if(hit.transform.root.TryGetComponent<MoteableObject>(out moteableObject))
            {
                moteableObject.HighLight(true);
                nowHighLight = moteableObject;
                if (Input.GetButtonDown("Fire1"))
                {
                    MusicSystem.PlayBGM();
                    StageSystem.instance.Spawn();
                    CountDownSystem.SetCount(true);
                    nowHighLight.Holded(this.transform);
                }
            }
        }
        else
        {
            if (nowHighLight == null) return;
            nowHighLight.HighLight(false);
        }
    }
}
