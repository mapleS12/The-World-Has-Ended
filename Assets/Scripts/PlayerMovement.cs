using UnityEngine;
using UnityEngine.UI;
using Terresquall; // Joystick namespace

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public VirtualJoystick joystick;
    public float jumpForce = 10f;
    public HoldButton jumpHoldButton;
    public Button interactButton;

    private Animator anim;
    private Rigidbody2D rb;
    private PlayerInteraction playerInteraction; // Reference to your interaction script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInteraction = GetComponent<PlayerInteraction>();
        interactButton.onClick.AddListener(Interact);
    }

    void Update()
    {
        if (joystick == null) return;

        Vector2 movement = joystick.GetAxis();
        transform.Translate(new Vector3(movement.x, 0, 0) * speed * Time.deltaTime);
        anim.SetBool("isWalking", Mathf.Abs(movement.x) > 0.01f);

        if (jumpHoldButton != null && jumpHoldButton.isPressed && movement.y < -0.5f)
        {
            Bend();
        }

        if (jumpHoldButton != null && jumpHoldButton.isPressed && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            Jump();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
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
        anim.SetTrigger("Interact");          // Play interact animation
        if (playerInteraction != null)
        {
            playerInteraction.TryInteract();  // Run your interaction code
        }
        Debug.Log("Player interacted!");
    }
}
