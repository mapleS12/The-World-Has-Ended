using UnityEngine;

// ONLY assign ItemType to collectible items in EnvironmentObject, if cleanable, no need for ItemData
public enum ItemType
{
    Artifact,
    Usable
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public string itemName;
    public string description;
    public ItemType itemType;
}