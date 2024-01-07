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

    [SerializeField]
    AudioSource hammerAudio;

    [SerializeField]
    GameObject[] particals;

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

        particalCooldown -= Time.deltaTime;
        PlayerState.charge = NowPower;

        if (timeout > 0.3f)
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



        if (!isSwing)
        {
            y = MapValue(y, -35f, 40f, -3f, 3f);

            float rotateY = y * 25f;


            VerticalRotation = rotateY;
            VerticalRotation = Mathf.Clamp(VerticalRotation, -70f, 85f);

                NowPower = power;
                NowPower = Mathf.Min(Mathf.Abs(NowPower), 10);

            if(y < -1f)
            {
                var addPower = Mathf.Min((Math.Abs(y)), 2.5f);
                power += y * Time.deltaTime * 4;
            }
            if(yDynamic < 0)
            {
                wantToSwing += Mathf.Abs(yDynamic);
                if(wantToSwing > 40) isSwing = true;
            }
        }
        else
        {
            y = MapValue(y, -25, 10, -3f, 3f);

            float rotateY = y * 25;


            VerticalRotation = rotateY;
            VerticalRotation = Mathf.Clamp(VerticalRotation, -70f, 85f);
            if(power != 0)
            {
                NowPower = power;
                NowPower = Mathf.Min(Mathf.Abs(NowPower), 10);
                power = 0;
            }
            if (yDynamic > 0)
            {
                wantToCharge += Mathf.Abs(yDynamic);
                if (wantToCharge > 3) isSwing = false;
            }
        }

        transform.eulerAngles = (defaultRotate + new Vector3(0, 0, VerticalRotation));

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
        if(isSwing)
        {
            hammerAudio.pitch = UnityEngine.Random.Range(0.7f, 1.2f);
            SpawnPartical(collision.contacts[0].point);
            hammerAudio.Play();
            cameraVibration.ShakeCamera();
            if(!collision.transform.GetComponentInParent<BananaAnimatorPlayer>().Hit(0.2f * (NowPower / 6.5f))) return;
            NowPower *= 0.8f;
        }
    }

    float particalCooldown;
    void SpawnPartical(Vector3 p)
    {
        if(particalCooldown > 0.1f) return;
        Destroy(Instantiate(particals[UnityEngine.Random.Range(0, 2)], p, Quaternion.identity), 2.5f);
        particalCooldown = 0.15f;
    }
}
