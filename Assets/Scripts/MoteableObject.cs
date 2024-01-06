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
        if(timeout > 1f)
        {
            // charge = 0;
            VerticalRotation = 0;
            transform.DOLocalRotate(defaultRotate, 1f);
        }else
        {
            transform.DOKill();
        }

        timeout += Time.deltaTime;
    }

    void Swing(float x, float y)
    {
        timeout = 0;
        VerticalRotation += -y * (y > 0 ? 2 : 3);
        VerticalRotation = Mathf.Clamp(VerticalRotation, -70f, 70f);
        transform.DOLocalRotate(defaultRotate + new Vector3(0, 0, VerticalRotation), 0.1f);
    }
    
    void Holded()
    {
        GetComponentInParent<FirstPersonController>().swingHead.AddListener(Swing);
    }
}
