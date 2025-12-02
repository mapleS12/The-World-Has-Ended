using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// this is a placeholder, i dont know your UI structure or how UI works.

public class InventoryUIManager : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Inventory targetInventory;     // Drag Player here
    public Transform slotParent;          // Parent of the UI slots
    public GameObject slotPrefab;         // UI slot prefab

    void Start()
    {
        RefreshUI();
    }

    // this rather than putting UI code in Inventory class.
    public void RefreshUI()
    {
        // todo: Clear old slots if needed
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // Make slots for each item in inventory
        foreach (var item in targetInventory.items)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            // TODO: Set slot UI elements based on item data n stuff, idk how to do that or how ur ui works
        }
    }
}
