using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BananaObject : MonoBehaviour
{
    bool isDead = false;
    bool isKnouk = false;
    BananaAnimatorPlayer[] bananas;
    
    [SerializeField]
    AudioSource hitSFX;
    
    [SerializeField]
    GameObject particle;

    Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        bananas = GetComponentsInChildren<BananaAnimatorPlayer>();
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.25f);
        transform.DORotate(new Vector3(0, 270, 0), 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isKnouk)
        {
            transform.Rotate(new Vector3(800, 0,0) * Time.deltaTime, Space.World);
            return;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            transform.eulerAngles += new Vector3(0, 200, 0) * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.E))
        {
            transform.eulerAngles -= new Vector3(0, 200, 0) * Time.deltaTime;
        }

        if(isDead) return;
        int live = 0;
        foreach(var b in bananas)
        {
            if(b.targetNormalizedTime <= 0.998f) live++;
        }
        if(live == 0) isDead = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(isDead && coroutine == null)
        {
            coroutine = StartCoroutine(KnockOut());
        }
    }


    IEnumerator KnockOut()
    {
        isKnouk = true;
        hitSFX.Play();
        Destroy(Instantiate(particle, transform.position + new Vector3(0, 2, 0), transform.rotation), 2.5f);
        transform.DOMove(new Vector3(2.5f, 11f, 38f), 2f).SetEase(Ease.OutQuart);
        PortalSystem.OpenPortal();
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(Vector3.zero, 1.5f);
        StageSystem.instance.Spawn();
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
}
