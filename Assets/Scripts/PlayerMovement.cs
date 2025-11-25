using UnityEngine;
using UnityEngine.UI;
using Terresquall; // Namespace for your joystick

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public VirtualJoystick joystick;
    public float jumpForce = 10f;
    public HoldButton jumpHoldButton; // For detecting jump press/hold
    public Button interactButton;

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        interactButton.onClick.AddListener(Interact);
    }

    void Update()
    {
        if (joystick == null) return;

        Vector2 movement = joystick.GetAxis();

        // Move the player horizontally
        transform.Translate(new Vector3(movement.x, 0, 0) * speed * Time.deltaTime);

        // Switch between Idle and Walk animation
        anim.SetBool("isWalking", Mathf.Abs(movement.x) > 0.01f);

        // For 'Bend': hold jump and joystick down
        if (jumpHoldButton != null && jumpHoldButton.isPressed && movement.y < -0.5f)
        {
            Bend();
        }

        // Jump logic: jump if holding jump and not already jumping
        if (jumpHoldButton != null && jumpHoldButton.isPressed && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
    }

    void Bend()
    {
        anim.SetTrigger("Bend");
        // Add collider or visual adjustments if needed
    }

    void Interact()
    {
        anim.SetTrigger("Interact");
        Debug.Log("Player interacted!");
    }
}
