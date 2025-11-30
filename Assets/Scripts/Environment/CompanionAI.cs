using UnityEngine;

public class CompanionAI : MonoBehaviour
{
    public Transform player;        // Drag the Player here
    public float speed = 3f;        // How fast it moves
    public float stopDistance = 1.5f; // How close it gets before stopping

    private void Update()
    {
        if (player == null) return;

        // 1. Calculate distance to player
        float distance = Vector2.Distance(transform.position, player.position);

        // 2. If too far away, move closer
        if (distance > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            
            // Optional: Flip sprite to face player
            if (player.position.x > transform.position.x)
                transform.localScale = new Vector3(1, 1, 1); // Face Right
            else
                transform.localScale = new Vector3(-1, 1, 1); // Face Left
        }
    }
}