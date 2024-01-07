using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaAnimatorPlayer : MonoBehaviour
{
    [SerializeField]
    string animationName;
    [SerializeField]
    float normalizedTime = 0;
    [SerializeField]
    Animator animator;

    [SerializeField]
    public float targetNormalizedTime = 0;

    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.Play(animationName, 0, Mathf.Lerp(normalizedTime, targetNormalizedTime, t));
        t += 1.5f * Time.deltaTime;
    }

    public bool Hit(float damage)
    {
        if(targetNormalizedTime >= 0.999) return false;
        if(t > 1)
        {
            normalizedTime = targetNormalizedTime;
        }
        targetNormalizedTime += damage;
        targetNormalizedTime = Mathf.Min(0.999f, targetNormalizedTime);
        t = 0;
        return true;
    }
}
