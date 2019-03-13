using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingScript : MonoBehaviour
{

    float timer;
    WorldGenerator generator;

    void Start()
    {
        generator = GameObject.FindGameObjectWithTag("Environment").GetComponent<WorldGenerator>();
        timer = Random.Range(10, 60);
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            generator.CreateTree(this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
