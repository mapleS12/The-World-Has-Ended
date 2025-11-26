using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // makes persistent across scenes
    void Awake()
    {
        // Prevent duplicates when switching scenes
        if (FindObjectsByType<QuestManager>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public RegionManager regionManager;

    public List<QuestData> activeQuests = new List<QuestData>();
    public List<QuestData> completedQuests = new List<QuestData>();

    public void StartQuest(QuestData quest)
    {
        if (quest.status == QuestStatus.Active || quest.status == QuestStatus.Completed)
        {
            Debug.Log("Quest already started or completed.");
            return;
        }

        quest.status = QuestStatus.Active;
        activeQuests.Add(quest);

        Debug.Log($"Quest Started: {quest.questName}");
    }

    public void CompleteObjective(string objectiveID)
    {
        foreach (QuestData quest in activeQuests)
        {
            foreach (QuestObjective objective in quest.objectives)
            {
                if (objective.objectiveID == objectiveID && !objective.isCompleted)
                {
                    objective.isCompleted = true;
                    Debug.Log($"Objective Completed: {objective.description}");

                    CheckQuestCompletion(quest);
                    return;
                }
            }
        }
    }

    private void CheckQuestCompletion(QuestData quest)
    {
        foreach (QuestObjective o in quest.objectives)
        {
            if (!o.isCompleted) return;
        }

        quest.status = QuestStatus.Completed;
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        Debug.Log($"Quest Completed: {quest.questName}");

        // region check
        RegionManager regionManager = FindFirstObjectByType<RegionManager>();
        if (regionManager != null)
        {
            regionManager.CheckRegionCompletion(quest.regionID);
        }
        else
        {
            Debug.LogWarning("No RegionManager found in scene.");
        }

    }
}
