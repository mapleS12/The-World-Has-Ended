using System.Collections.Generic;
using UnityEngine;

/* ===========================
   Inventory (GAMEPLAY) -- DO NOT PUT UI HERE
   ===========================
   Purpose:
   - This Inventory stores the actual items used by gameplay (quests, checks, interactions).
   - It must remain the single source-of-truth for item storage and logic (AddItem, HasItem, etc).

   What UI ppl should do:
   - Do NOT add slot prefabs, UI loops, or UI updating logic inside this class.
   - Subscribe to the provided events (OnItemAdded, OnInventoryChanged) from a separate UI script.
   - Use Inventory.items (read-only) to render slots.

   If you need: create a new script `InventoryUIManager.cs` that uses the Inventory API.
   Any UI changes that touch storage, counting, stacking, or AddItem logic must be discussed first.

   Keeping model & view separate prevents breaking quests/interaction logic.
*/

public class Inventory : MonoBehaviour
{
    // Must be the ONLY place where collected items are stored for gameplay logic.
    public List<ItemData> items = new List<ItemData>();

    // Add item. Cant collect duplicate items
    public bool AddItem(ItemData item)
    {
        if (items.Contains(item))
        {
            Debug.Log("Item already collected.");
            return false;
        }

        items.Add(item);
        Debug.Log($"Collected: {item.itemName}");
        return true;
    }

    // Check if item is in inventory by itemID (used for quests, interactions, etc.)
    public bool HasItem(string itemID)
    {
        return items.Exists(i => i.itemID == itemID);
    }
}
