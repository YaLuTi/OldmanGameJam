using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    static AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayBGM()
    {
        bgm.Play();
    }
}
