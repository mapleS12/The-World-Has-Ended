using UnityEngine;

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