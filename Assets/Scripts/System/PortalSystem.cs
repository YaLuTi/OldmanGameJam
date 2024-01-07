using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalSystem : MonoBehaviour
{
    static bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger) StartCoroutine(open());
    }

    public static void OpenPortal()
    {
        trigger = true;
    }

    IEnumerator open()
    {
        trigger = false;
        transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.75f);
        yield return new WaitForSeconds(1.5f);
        transform.DOScale(new Vector3(0,0,0), 0.25f);
        yield return null;
    }
}
