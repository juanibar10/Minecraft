using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public enum blockTypes
    {
        Solid,
        Gravity,
        Leaves
    }

    public blockTypes blockBehaviours;
    public  int id;
    
    private Inventory inv;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();     
        if(blockBehaviours == blockTypes.Gravity)
        {
            gameObject.AddComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreackBlock()
    {
        if (blockBehaviours == blockTypes.Leaves)
        {
            int chance = Random.Range(0, 100);
            if (chance > 90)
            {
                inv.AddItem(id);
            }
            Destroy(this.gameObject);

        }
        else
        {
            inv.AddItem(id);
            Destroy(this.gameObject);
        }
    }

    
}
