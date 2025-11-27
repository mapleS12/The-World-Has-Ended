using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RegionData
{
    public string regionID;
    public List<QuestData> regionQuests;
    public bool isUnlocked;
}

public class RegionManager : MonoBehaviour
{
    public List<RegionData> regions;

    public void CheckRegionCompletion(string regionID)
    {
        RegionData region = regions.Find(r => r.regionID == regionID);

        if (region == null)
        {
            Debug.Log("Region not found.");
            return;
        }

        foreach (QuestData quest in region.regionQuests)
        {
            if (quest.status != QuestStatus.Completed)
            {
                Debug.Log($"Region {regionID} is not yet completed.");
                return;
            }
        }

        region.isUnlocked = true;
        Debug.Log($"Region {regionID} completed and unlocked!");
    }
}