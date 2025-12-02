using UnityEngine;

public class CompanionAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float stopDistance = 2f;

    [Header("Appearance")]
    public float sizeMultiplier = 1.5f;   
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        transform.localScale = Vector3.one * sizeMultiplier;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            if (player.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;
        }
    }
}