using UnityEngine;

[System.Serializable]
public class QuestObjective
{
    [Tooltip("Unique identifier for this objective.")]
    public string objectiveID; // unique id
    [Tooltip("Displayed text to the player.")]
    public string description;
    [Tooltip("Automatically updated by QuestManager.")]
    public bool isCompleted;
}