using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : MonoBehaviour
{
    public static StageSystem instance;
    [SerializeField]
    GameObject[] gameObjects;

    int count = 0;

    [SerializeField]
    Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        if(count >= 5)
        {
            CountDownSystem.SetCount(false);
        }
        Instantiate(gameObjects[count], spawnPoint.position, Quaternion.identity);
        count++;
    }
}
