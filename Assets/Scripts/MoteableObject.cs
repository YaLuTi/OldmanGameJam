using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MoteableObject : MonoBehaviour
{

    public float Dynamic;

    float VerticalRotation;

    float timeout = 1;

    Vector3 defaultRotate;
    Animator animator;

    [SerializeField]
    Vector3 holdPos, holdEulerAngle;

    [SerializeField]
    Outline[] outlines;

    public float power;

    public bool isSwing = false;
    public float wantToSwing = 0;

    public float wantToCharge = 0;

    bool isHold = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotate = transform.eulerAngles;
        outlines = GetComponentsInChildren<Outline>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHold) return;

        PlayerState.charge = NowPower;

        if (timeout > 0.4f)
        {
            VerticalRotation = 0;
            power = 0;
            isSwing = false;
            transform.DOLocalMove(holdPos, 1f);
            transform.DOLocalRotate(defaultRotate, 1f);
            wantToSwing = 0;
        }
        else
        {
            transform.DOKill();
        }

        timeout += Time.deltaTime;

        if (MathF.Abs(power) > 6 && !isSwing)
        {
            animator.Play("Banana");
        }else
        {
            animator.Play("End");
        }
    }

    void Swing(float yDynamic, float y)
    {
        timeout = 0;

        y = MapValue(y, -35, 10, -3f, 3f);

        float rotateY = y * 25;


        VerticalRotation = rotateY;
        VerticalRotation = Mathf.Clamp(VerticalRotation, -70f, 85f);


        if (!isSwing)
        {
            if(y < -1f)
            {
                var addPower = Mathf.Min((Math.Abs(y)), 2.5f);
                power += y / 30f;
                NowPower = power;
                NowPower = Mathf.Min(Mathf.Abs(NowPower), 10);
            }
            if(yDynamic < 0)
            {
                wantToSwing += Mathf.Abs(yDynamic);
                if(wantToSwing > 30) isSwing = true;
            }
        }
        else
        {
            NowPower = power;
            power = 0;
            if (yDynamic > 0)
            {
                wantToCharge += Mathf.Abs(yDynamic);
                if (wantToCharge > 3) isSwing = false;
            }
        }

        transform.DOLocalRotate(defaultRotate + new Vector3(0, 0, VerticalRotation), 0.1f);

    }

    float NowPower;
    
    public void Holded(Transform parent)
    {
        isHold = true;
        parent.GetComponent<FirstPersonController>().swingHead.AddListener(Swing);
        this.transform.parent = parent;
        transform.DOLocalMove(holdPos, 0.3f);
        transform.DOLocalRotate(holdEulerAngle, 0.3f);

        foreach (var outline in outlines)
        {
            outline.enabled = false;
        }
    }

    public void HighLight(bool isHighlight)
    {
        if(isHighlight)
        {
            foreach (var outline in outlines)
            {
                outline.enabled = true;
            }
        }
        else
        {
            foreach (var outline in outlines)
            {
                outline.enabled = false;
            }
        }
    }

    static float MapValue(float inputValue, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        float inputRange = inputMax - inputMin;
        float outputRange = outputMax - outputMin;

        float mappedValue = outputMin + (inputValue - inputMin) / inputRange * outputRange;

        return Math.Min(Math.Max(mappedValue, outputMin), outputMax);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
