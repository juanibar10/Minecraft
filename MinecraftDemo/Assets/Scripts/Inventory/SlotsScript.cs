using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotsScript : MonoBehaviour, IDropHandler
{
    Inventory inv;

    public int slotNumber;
    public string itemName;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    
    void Update()
    {
        itemName = inv.items[slotNumber].Name;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if(inv.items[slotNumber].ID == -1)
        {
            inv.items[droppedItem.currentSlot] = inv.database.GetItemById(-1);
            inv.items[slotNumber] = droppedItem.item;
            droppedItem.currentSlot = slotNumber;
        }
        else if(droppedItem.currentSlot != slotNumber)
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().currentSlot = droppedItem.currentSlot;
            item.transform.SetParent(inv.slots[droppedItem.currentSlot].transform);
            item.transform.position = inv.slots[droppedItem.currentSlot].transform.position;

            inv.items[droppedItem.currentSlot] = item.GetComponent<ItemData>().item;
            inv.items[slotNumber] = droppedItem.item;

            droppedItem.currentSlot = slotNumber;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;
        }
    }
}
