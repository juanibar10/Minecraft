using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct Item
{
    public int ID;
    public string Name;
    public bool Stackable;
    public bool Placeable;
    public string Slug;
    public Sprite Sprite;
    public GameObject Object;

    public Item(int id, string name, bool stackable, bool placeable, string slug)
    {
        ID = id;
        Name = name;
        Stackable = stackable;
        Slug = slug;
        Placeable = placeable;
        Sprite = Resources.Load<Sprite>("Sprites/" + slug);
        Object = Resources.Load<GameObject>("Blocks/" + name + "Block");
    }
}


public class ItemDatabase : MonoBehaviour
{

    public List<Item> itemDatabase = new List<Item>();

    private void Start()
    {
        GetDatabase("Assets/Resources/ItemData.txt");
    }

    public void GetDatabase(string path)
    {
        StreamReader sr = new StreamReader(path);

        AddItem:
        
        itemDatabase.Add(new Item(
            int.Parse(sr.ReadLine().Replace("id: ", "")),
            sr.ReadLine().Replace("name: ", ""),
            bool.Parse(sr.ReadLine().Replace("stackable: ","")),
            bool.Parse(sr.ReadLine().Replace("placeable: ", "")),
            sr.ReadLine().Replace("slug: ","")
            ));

        string c = sr.ReadLine();
        if(c == ",")
            goto AddItem;
        else if(c == ";")
            sr.Close();
        else
            Debug.LogError("ERROR: La base de datos de items no esta correctamente escrita.");

    }

    public Item GetItemById(int id)
    {
        for(int i = 0; i < itemDatabase.Count; i++)
        {
                if(itemDatabase[i].ID == id)
            {
                return itemDatabase[i];
            }
        }
        return itemDatabase[0];
    }
    public Item GetItemByName(string Name)
    {
        for (int i = 0; i < itemDatabase.Count; i++)
        {
            if (itemDatabase[i].Name == Name)
            {
                return itemDatabase[i];
            }
        }
        return itemDatabase[0];
    }
}
