using UnityEngine;

public class ReachLocationTrigger : MonoBehaviour
{
    [Tooltip("Unique identifier attached to the relevant quest objective.")]
    public string objectiveID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager qm = FindFirstObjectByType<QuestManager>();
            if (qm != null)
            {
                qm.CompleteObjective(objectiveID);
                Destroy(gameObject); // remove so it doesn't trigger again
            }
        }
    }
}
/* HOW TO USE:
 1. Create an empty GameObject
 2. Add BoxCollider2D --> check IsTrigger
 3. Add ReachLocationTrigger.cs component
 4. Fill in relevant objectiveID */