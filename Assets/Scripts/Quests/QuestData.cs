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
    public string questID; // unique id
    public string questName;
    public string regionID; // ties quest to a specific region
    public string questDescription;

    public List<QuestObjective> objectives = new List<QuestObjective>();

    [HideInInspector] public QuestStatus status = QuestStatus.Inactive;
}
