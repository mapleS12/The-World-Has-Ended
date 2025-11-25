using UnityEngine;
using UnityEngine.UI;
using Terresquall; // Your joystick's namespace

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public VirtualJoystick joystick;
    public float jumpForce = 10f;
    public HoldButton jumpHoldButton; // NOT Button
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
        transform.Translate(new Vector3(movement.x, 0, 0) * speed * Time.deltaTime);
        anim.SetBool("isWalking", Mathf.Abs(movement.x) > 0.01f);

        // For bend: jump button held & joystick down
        if (jumpHoldButton.isPressed && movement.y < -0.5f)
        {
            Bend();
        }

        // Optional: Jump on press (hold logic handled by button events/UI)
        if (jumpHoldButton.isPressed && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
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
        // Example: reduce collider height here, if wanted
    }
    void Interact()
    {
        anim.SetTrigger("Interact");
        Debug.Log("Player interacted!");
    }
}
