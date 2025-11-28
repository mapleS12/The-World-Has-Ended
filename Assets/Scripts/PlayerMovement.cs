/*
using UnityEngine;
using UnityEngine.UI;
using Terresquall; // Virtual Joystick namespace

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Joystick Input")]
    public VirtualJoystick joystick;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public HoldButton jumpHoldButton;

    [Header("Interaction")]
    public Button interactButton;

    private Animator anim;
    private Rigidbody2D rb;
    private PlayerInteraction playerInteraction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInteraction = GetComponent<PlayerInteraction>();

        if (interactButton != null)
            interactButton.onClick.AddListener(Interact);
    }

    void Update()
    {
        // --------------------------
        // SAFETY CHECKS (Prevents NaN)
        // --------------------------

        // If joystick missing → do nothing.
        if (joystick == null)
        {
            Debug.LogWarning("No joystick assigned to PlayerMovement.");
            anim.SetBool("isWalking", false);
            return;
        }

        // Get joystick axis
        Vector2 movement = joystick.GetAxis();

        // Prevent NaN from joystick plugin errors
        if (float.IsNaN(movement.x) || float.IsNaN(movement.y))
        {
            movement = Vector2.zero; // stop movement
            Debug.LogWarning("Joystick returned NaN — forcing movement = zero.");
        }

        // --------------------------
        // MOVEMENT
        // --------------------------
        Vector3 moveVector = new Vector3(movement.x, 0, 0);
        transform.Translate(moveVector * speed * Time.deltaTime);

        anim.SetBool("isWalking", Mathf.Abs(movement.x) > 0.01f);

        // --------------------------
        // BEND (downward joystick)
        // --------------------------
        if (jumpHoldButton != null &&
            jumpHoldButton.isPressed &&
            movement.y < -0.5f)
        {
            Bend();
        }

        // --------------------------
        // JUMP
        // --------------------------
        if (jumpHoldButton != null &&
            jumpHoldButton.isPressed &&
            Mathf.Abs(rb.linearVelocity.y) < 0.01f)
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
    }

    void Interact()
    {
        anim.SetTrigger("Interact");

        if (playerInteraction != null)
            playerInteraction.TryInteract();

        Debug.Log("Player interacted!");
    }
}
*/
