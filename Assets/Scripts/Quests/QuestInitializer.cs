using System.Collections.Generic;
using UnityEngine;


// this is for the tutorial level only, it auto starts the quests on scene load, which is not what we would do for level 1 or future levels.
public class QuestInitializer : MonoBehaviour
{
    public List<QuestData> tutorialQuests;

    void Start()
    {
        QuestManager qm = FindFirstObjectByType<QuestManager>();

        if (qm == null)
        {
            Debug.LogError("QuestManager not found in scene!");
            return;
        }

        foreach (QuestData q in tutorialQuests)
        {
            qm.StartQuest(q);
        }
    }
}
