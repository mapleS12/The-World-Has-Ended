using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    // in unity make an empty game object, name it QuestTrigger, attach this script to it, and assign your quest scriptable object to it
    // then later we can attach this script to a dialogue, interaction, trigger zone, or npc to trigger the start of a quest.
    public QuestData quest;

    public void TriggerQuest()
    {
        FindFirstObjectByType<QuestManager>().StartQuest(quest);
    }
}
