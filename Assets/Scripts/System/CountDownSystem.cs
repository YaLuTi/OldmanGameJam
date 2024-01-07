using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownSystem : MonoBehaviour
{
    static bool counting = false;
    float time = 0;

    [SerializeField]
    TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        counting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(counting) time += Time.deltaTime;
        string formattedString = time.ToString("00.00");
        textMeshPro.text = formattedString;
    }

    public static void SetCount(bool isCount)
    {
        counting = isCount;
    }
}
