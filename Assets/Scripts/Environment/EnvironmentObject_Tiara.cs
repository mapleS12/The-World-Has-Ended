using UnityEngine;
//alr declared in the og Env Obj
/*public enum EnvironmentObjectType
{
    [Tooltip("Adds to inventory.")]
    Collectible, 
    [Tooltip("Disappears only. Does not add to inventory.")]
    Cleanable 
}*/

public class EnvironmentObject_Tiara : MonoBehaviour
{
    public EnvironmentObjectType objectType = EnvironmentObjectType.Collectible;
    
    [Tooltip("Only needed if collectible. Leave blank if cleanable.")]
    public ItemData itemData; 
    
    [Tooltip("Leave blank if no tool required.")]
    public string requiredItemID; 
    
    [Tooltip("Unique identifier attached to quest.")]
    public string objectiveID; 

    // This is the Trigger logic "Fixed up" for touching
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Interact(Inventory.Instance);
        }
    }

    // This is your friend's Quest Logic (Preserved)
    public void Interact(Inventory inventory)
    {
        QuestManager qm = FindFirstObjectByType<QuestManager>();

        // 1. Check Required Tool
        if (!string.IsNullOrEmpty(requiredItemID) && !inventory.HasItem(requiredItemID))
        {
            Debug.Log($"You need {requiredItemID} to interact with this.");
            return;
        }

        // 2. Handle Interaction Type
        switch (objectType)
        {
            case EnvironmentObjectType.Collectible:
                if (itemData == null) return;

                // Attempt to add to inventory
                if (inventory.AddItem(itemData))
                {
                    Debug.Log($"Collected: {itemData.itemName}");

                    // Quest Update
                    if (!string.IsNullOrEmpty(objectiveID) && qm != null)
                    {
                        qm.AddProgressToObjective(objectiveID, 1);
                    }

                    Destroy(gameObject);
                }
                break;

            case EnvironmentObjectType.Cleanable:
                Debug.Log("Trash cleaned!");

                // Quest Update
                if (!string.IsNullOrEmpty(objectiveID) && qm != null)
                {
                    qm.AddProgressToObjective(objectiveID, 1);
                }

                Destroy(gameObject);
                break;
        }
    }
}