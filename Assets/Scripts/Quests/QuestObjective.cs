using UnityEngine;

public enum ObjectiveType
{
    [Tooltip("Collect a specific item.")]
    CollectItem,
    [Tooltip("Clean specific trash.")]
    CleanTrash,
    [Tooltip("Walk into a trigger.")]
    ReachLocation,
    [Tooltip("Collect X amount of items.")]
    CollectMultiple,
    [Tooltip("Clean X amount of trash.")]
    CleanMultiple,
    [Tooltip("General-purpose trigger.")]
    TriggerEvent,
}

[System.Serializable]
public class QuestObjective
{
    [Tooltip("Unique identifier for this objective.")]
    public string objectiveID; // unique id
    [Tooltip("Displayed text to the player.")]
    public string description;
    [Tooltip("Type of objective to complete.")]
    public ObjectiveType objectiveType;

    [Tooltip("Automatically updated by QuestManager.")]
    public bool isCompleted;

    // for multi-count objectives:
    [Tooltip("Number of items/trash to collect/clean.")]
    public int requiredCount; // e.g., collect 5 items
    [Tooltip("Current count of items/trash collected/cleaned.")]
    public int currentCount; // updated as player progresses

    // for reach location objectives:
    [Tooltip("Target location to reach.")]
    public Transform targetLocation; // set in inspector or dynamically
}