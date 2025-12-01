// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;

// public class InventoryManager : MonoBehaviour
// {
    
//     public static InventoryManager Instance;

//     // The actual data (your backpack)
//     public List<ItemData> items = new List<ItemData>();

//     public Transform slotParent;
//     public GameObject slotPrefab;

//     private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(this);
//         }
//         else
//         {
//             Instance = this;
//         }
//     }

//     public bool AddItem(ItemData newItem)
//     {
//         items.Add(newItem);
//         UpdateUI();
//         return true;
//     }

//     public void UpdateUI()
//     {
//         foreach (Transform child in slotParent)
//         {
//             Destroy(child.gameObject);
//         }

//         foreach (ItemData item in items)
//         {
//             GameObject newSlot = Instantiate(slotPrefab, slotParent);
//             Image icon = newSlot.transform.Find("Icon").GetComponent<Image>();
//             icon.sprite = item.icon;
//             icon.enabled = true; 
//         }
//     }
// }