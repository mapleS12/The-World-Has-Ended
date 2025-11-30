using UnityEngine;

public enum EnvironmentObjectType
{
    [Tooltip("Adds to inventory.")]
    Collectible, // adds to inventory
    [Tooltip("Disappears only. Does not add to inventory.")]
    Cleanable // disappears only
}
public class EnvironmentObject : MonoBehaviour
{
    public EnvironmentObjectType objectType = EnvironmentObjectType.Collectible;
    [Tooltip("Only needed if collectible. Leave blank if cleanable.")]
    public ItemData itemData; // only needed if collectible
    [Tooltip("Leave blank if no tool required.")]
    public string requiredItemID; // leave blank if no tool required for interaction
    [Tooltip("Unique identifier attached to quest.")]
    public string objectiveID; // for quest objectives

    public void Interact(Inventory inventory)
    {
        QuestManager qm = FindFirstObjectByType<QuestManager>();

        // REQUIRE TOOL FIRST (IF needed)
        if (!string.IsNullOrEmpty(requiredItemID) && !inventory.HasItem(requiredItemID))
        {
            Debug.Log($"You need {requiredItemID} to interact with this.");
            return;  // EARLY EXIT � prevents bad objective completion
        }

        // PROCEED BASED ON TYPE
        switch (objectType)
        {
            case EnvironmentObjectType.Collectible:
                if (itemData == null)
                {
                    Debug.LogWarning("Collectible missing ItemData!");
                    return;
                }

                if (inventory.AddItem(itemData))
                {
                    Debug.Log($"Collected: {itemData.itemName}");

                    // quest update
                    if (!string.IsNullOrEmpty(objectiveID) && qm != null)
                    {
                        qm.AddProgressToObjective(objectiveID, 1);
                    }

                    Destroy(gameObject);
                }
                break;

            case EnvironmentObjectType.Cleanable:
                Debug.Log("Trash cleaned!");

                // quest update
                if (!string.IsNullOrEmpty(objectiveID) && qm != null)
                {
                    qm.AddProgressToObjective(objectiveID, 1);
                }

                Destroy(gameObject);
                break;
        }
    }
}