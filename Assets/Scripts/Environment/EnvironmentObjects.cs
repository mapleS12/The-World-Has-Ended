using UnityEngine;

public enum EnvironmentObjectType
{
    Collectible, // adds to inventory
    Cleanable // disappears only
}
public class EnvironmentObject : MonoBehaviour
{
    public EnvironmentObjectType objectType = EnvironmentObjectType.Collectible;
    public ItemData itemData; // only needed if collectible
    public string requiredItemID; // leave blank if no tool required for interaction

    public void Interact(Inventory inventory)
    {
        // If trash requires broom or tool
        if (!string.IsNullOrEmpty(requiredItemID) && !inventory.HasItem(requiredItemID))
        {
            Debug.Log($"You need {requiredItemID} to interact with this.");
            return;
        }

        switch (objectType)
        {
            case EnvironmentObjectType.Collectible:
                if (itemData == null)
                {
                    Debug.LogWarning("Collectible missing ItemData!");
                    return;
                }

                // If successfully added, remove object from scene
                if (inventory.AddItem(itemData))
                {
                    Destroy(gameObject);
                }
                break;

            case EnvironmentObjectType.Cleanable:
                Debug.Log("Trash cleaned!");
                Destroy(gameObject);
                break;
        }
    }
}