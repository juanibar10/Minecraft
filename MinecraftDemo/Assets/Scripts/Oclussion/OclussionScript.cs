using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OclussionScript : MonoBehaviour
{
    public int rayAmount = 2500;
    public int rayDistance = 300;
    public float stayTime = 2;

    public Camera camara;

    public Vector2[] rayPoints;
    public LayerMask layer;

    
    void Start()
    {
        camara = gameObject.GetComponent<Camera>();
        rayPoints = new Vector2[rayAmount];
        GetPoints();
    }

    
    void Update()
    {
        CastRays();
    }

    void GetPoints()
    {
        float x = 0;
        float y = 0;
        
        for(int i = 0; i< rayAmount; i++)
        {
            if(x > 1)
            {
                x = 0;
                y += 1 / Mathf.Sqrt(rayAmount);
            }

            rayPoints[i] = new Vector2(x, y);
            x += 1 / Mathf.Sqrt(rayAmount);
        }
    }

    void CastRays()
    {
        for(int i = 0; i < rayAmount; i++)
        {
            Ray ray;
            RaycastHit hit;

            OclussionObject ocl;

            ray = camara.ViewportPointToRay(new Vector3(rayPoints[i].x, rayPoints[i].y, 0));

            if(Physics.Raycast(ray, out hit, rayDistance,layer))
            {
                if(ocl = hit.transform.GetComponent<OclussionObject>())
                {
                    ocl.HitOclude(stayTime);
                }
            }
        }
    }
}
