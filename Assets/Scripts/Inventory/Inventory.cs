using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    public GameObject inventoryUI;
    // Add your UI slots later...

    public bool AddItem(ItemData item)
    {
        if (items.Contains(item))
        {
            Debug.Log("Item already collected.");
            return false;
        }

        items.Add(item);
        Debug.Log($"Collected: {item.itemName}");

        SafeUpdateUI();
        return true;
    }

    public bool HasItem(string itemID)
    {
        return items.Exists(i => i.itemID == itemID);
    }

    void SafeUpdateUI()
    {
        try
        {
            if (inventoryUI == null)
            {
                return;
            }

            // TODO: your real UI update later...
        }
        catch
        {
            Debug.LogWarning("Inventory UI update failed but was safely ignored.");
        }
    }
}
