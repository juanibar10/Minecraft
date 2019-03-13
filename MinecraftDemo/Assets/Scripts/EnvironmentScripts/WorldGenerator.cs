using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    public GameObject player;

    public int sizeX;
    public int sizeZ;


    public int groundHeight;
    public float terDetail;
    public float terHeight;
    private int seed;
    private BlockScript blockScript;
    public GameObject[] blocks;
    public Material material;

    public GameObject environment;
    
    void Start()
    {
        seed = Random.Range(100000, 999999);
        GenerateTerrain();
        
    }

    public void GenerateTerrain()
    {
        for(int x = 0; x < sizeX; x++)
        {
            for(int z = 0; z < sizeZ; z++)
            {
                int maxY = (int)(Mathf.PerlinNoise((x / 2 + seed) /terDetail,(z / 2 + seed) / terDetail)*terHeight);

                maxY += groundHeight;

                GameObject grass = Instantiate(blocks[0], new Vector3(x, maxY, z), Quaternion.identity)as GameObject;
                grass.transform.SetParent(environment.transform);

                for(int y = 0; y < maxY; y++)
                {

                    int dirtLayers = Random.Range(1, 8);
                    if(y >= maxY - dirtLayers)
                    {
                        GameObject dirt = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        dirt.transform.SetParent(environment.transform);
                        int chance = Random.Range(0, 100);
                        if(y == maxY -1 && chance < 2)
                        {
                            CreateTree(new Vector3(x, y + 2, z));
                        }
                    }
                    else
                    {
                        GameObject stone = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        stone.transform.SetParent(environment.transform);
                    }

                    
                }

                if(x == (int)(sizeX / 2) && z == (int)(sizeZ / 2))
                {
                    Instantiate(player, new Vector3(x, maxY + 3, z), Quaternion.identity);
                }
            }
        }
    }

    public void CreateTree(Vector3 pos)
    {
        GameObject tree = new GameObject();
        tree.transform.SetParent(environment.transform);
        tree.name = "tree";


        //Madera
        int height = Random.Range(4, 7);
        for (int i = 0; i < height; i++)
        {
            GameObject wood = Instantiate(blocks[4], new Vector3(pos.x, pos.y + i, pos.z), Quaternion.identity);
            wood.transform.SetParent(environment.transform);
        }

        //Hojas

        float radius = ((float)height/3)* 2;
        Vector3 center = new Vector3(pos.x, pos.y + height - 1, pos.z);
        for (int i = -(int)radius; i < radius; i++)
        {
            for (int j = 0; j < radius; j++)
            {
                for (int k = -(int)radius; k < radius; k++)
                {
                    Vector3 position = new Vector3(i+center.x, j+ center.y, k+center.z);
                    float distance = Vector3.Distance(center, position);
                    if(distance < radius)
                    {
                        if(!Physics.CheckBox(position, new Vector3(0.1f,0.1f, 0.1f)))
                        {
                            GameObject leaf = Instantiate(blocks[5], position, Quaternion.identity);
                            leaf.transform.SetParent(environment.transform);
                        }
                    }
                }
            }
        }
    }

    
}
