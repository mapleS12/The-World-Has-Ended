using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    // in my requirements, cant collect same artifact twice
    public bool AddItem(ItemData item)
    {
        if(items.Contains(item))
        {
        Debug.Log("Item already collected.");
        return false;
        }

        items.Add(item);
        Debug.Log($"Collected: {item.itemName}");
        return true;
    }

    public bool HasItem(string itemID)
    {
        return items.Exists(i => i.itemID == itemID);
    }
}