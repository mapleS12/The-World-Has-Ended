using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RegionData
{
    [Tooltip("Unique region identifier")]
    public string regionID;
    [Tooltip("Tag a Quest ScriptableObject here.")]
    public List<QuestData> regionQuests;
    [Tooltip("Only manually assign to tutorial level. Will automatically update to Unlocked once region quests are completed.")]
    public bool isUnlocked;
    [Tooltip("The region unlocked when this one is completed.")]
    public string nextRegionID;
}

public class RegionManager : MonoBehaviour
{
    public List<RegionData> regions;
    public System.Action<string> OnRegionProgressUpdated; // for UI updates, etc.

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

                // Notify UI or other systems about progress update.
                OnRegionProgressUpdated?.Invoke(regionID);

                return;
            }
        }

        // region.isUnlocked = true; redundant, alr unlocked if completed.
        Debug.Log($"Region {regionID} completed!");

        // Notify UI or other systems about progress update.
        OnRegionProgressUpdated?.Invoke(regionID);

        // Unlock next region (if any)
        if (!string.IsNullOrEmpty(region.nextRegionID))
        {
            RegionData nextRegion = regions.Find(r => r.regionID == region.nextRegionID);

            if (nextRegion != null)
            {
                nextRegion.isUnlocked = true;
                Debug.Log($"Next region unlocked: {nextRegion.regionID}");
            }
            else
            {
                Debug.Log($"Next Region ID '{region.nextRegionID}' not found in RegionManager.");
            }
        }
    }
    // Helper method for unlocking door to next region
    public bool IsRegionUnlocked(string regionID)
    {
        RegionData region = regions.Find(r => r.regionID == regionID);
        return region != null && region.isUnlocked;
    }
    /* When we have a door thing for tutorial exit, we
      would need the script to do something like:

       if (FindFirstObjectByType<RegionManager>().IsRegionUnlocked("level1")) {
            // allow leaving tutorial level, onto level 1.
       }
       else
       {
            Debug.Log("Level 1 is locked.");
       }
    */

    public float GetRegionProgress(string regionID)
    {
        RegionData region = regions.Find(r => r.regionID == regionID);
        if (region == null)
        {
            Debug.LogWarning($"Region '{regionID}' not found.");
            return 0f;
        }

        int totalObjectives = 0;
        int completedObjectives = 0;

        foreach (QuestData quest in region.regionQuests)
        {
            if (quest == null) continue;

            foreach (QuestObjective obj in quest.objectives)
            {
                if (obj == null) continue;

                totalObjectives++;

                if (obj.isCompleted)
                    completedObjectives++;
            }
        }

        if (totalObjectives == 0)
            return 0f; // avoids divide by zero

        return (float)completedObjectives / totalObjectives;
    }

    public int GetRegionProgressPercent(string regionID)
    {
        return Mathf.RoundToInt(GetRegionProgress(regionID) * 100f);
    }

}