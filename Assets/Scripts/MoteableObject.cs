using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoteableObject : MonoBehaviour
{
    public float Dynamic;

    float VerticalRotation;

    float timeout = 0;

    Vector3 defaultRotate;
    // Start is called before the first frame update
    void Start()
    {
        Holded();
        defaultRotate = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeout > 2f)
        {
            // charge = 0;
            VerticalRotation = 0;
            transform.DORotate(defaultRotate, 1f);
        }

        timeout += Time.deltaTime;
    }

    void Swing(float x, float y)
    {
        timeout = 0;
        VerticalRotation += -y * 3;
        VerticalRotation = Mathf.Clamp(VerticalRotation, -70f, 70f);
        transform.DORotate(defaultRotate + new Vector3(0, 0, VerticalRotation), 0.1f);
    }
    
    void Holded()
    {
        GetComponentInParent<FirstPersonController>().swingHead.AddListener(Swing);
    }
}
