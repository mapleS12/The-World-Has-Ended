using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    // The Data (Your Backpack)
    public List<ItemData> items = new List<ItemData>();

    public Transform slotParent;
    public GameObject slotPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool AddItem(ItemData newItem)
    {
        if (items.Contains(newItem))
        {
            Debug.Log("Item already collected.");
            return false;
        }

        items.Add(newItem);
        Debug.Log($"Collected: {newItem.itemName}");

        UpdateUI();

        return true;
    }

        public bool HasItem(string itemID)
    {
        return items.Exists(i => i.itemID == itemID);
    }

    public void UpdateUI()
    {
        // Clear old slots
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // Draw new slots
        foreach (ItemData item in items)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            Image icon = newSlot.transform.Find("Icon").GetComponent<Image>();
            icon.sprite = item.icon;
            icon.enabled = true;
        }
    }

    public void ToggleInventory()
    {
        bool isActive = slotParent.gameObject.activeSelf;
        slotParent.gameObject.SetActive(!isActive);
    }
}