using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    public bool isCollectible = true;
    public ItemData itemData;

    public void Interact(Inventory inventory)
    {
        if (!isCollectible)
        {
            Debug.Log("Not collectible.");
            return;
        }

        if (itemData == null)
        {
            Debug.LogWarning("Missing ItemData!");
            return;
        }

        if (inventory.AddItem(itemData))
        {
            Destroy(gameObject);
        }
    }
}