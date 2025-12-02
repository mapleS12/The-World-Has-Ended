using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus
{
    Inactive,
    Active,
    Completed
}

[CreateAssetMenu(fileName = "NewQuest", menuName = "Game/Quest")]
public class QuestData : ScriptableObject
{
    [Tooltip("Unique identifier for this quest.")]
    public string questID; // unique id
    [Tooltip("Name of the quest as displayed to the player in inventory.")]
    public string questName;
    [Tooltip("Ties quest to a specific region.")]
    public string regionID; // ties quest to a specific region
    [Tooltip("Description of the quest as displayed to the player in inventory.")]
    public string questDescription;

    [Tooltip("List of objectives that need to be completed for this quest.")]
    public List<QuestObjective> objectives = new List<QuestObjective>();

    [HideInInspector] public QuestStatus status = QuestStatus.Inactive;
}
