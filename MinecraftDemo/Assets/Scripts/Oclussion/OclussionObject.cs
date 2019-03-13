using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OclussionObject : MonoBehaviour
{

    Renderer renderer;
    public float displayTime;

    private void OnEnable()
    {
        renderer = gameObject.GetComponent<Renderer>();
        displayTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(displayTime > 0)
        {
            displayTime -= Time.deltaTime;
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
        }
    }

    public void HitOclude(float time)
    {
        displayTime = time;

        renderer.enabled = true;
    }
}
