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

    public void Interact(Inventory inventory)
    {
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