using UnityEngine;
using UnityEngine.UI;
using Terresquall;  // 🔥 IMPORTANT: your joystick lives in this namespace

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public VirtualJoystick joystick;  
    public float jumpForce = 10f;
    public Button jumpButton;
    public Button interactButton;
    public Toggle bendToggle;

    private Rigidbody2D rb;

    bool IsJumpHeld()
    {
        bool jumpHeld = jumpButton.isPressed;
        return jumpHeld;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpButton.onClick.AddListener(Jump);
        interactButton.onClick.AddListener(Interact);
    }    
    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f  && jumpButton.isPressed())
        {
            
            GetComponent<Animator>().SetTrigger("Jump");
        }
    }
    void Bend()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            GetComponent<Animator>().SetTrigger("Bend");
            var collider = GetComponent<BoxCollider2D>();
            collider.size = new Vector2(collider.size.x, collider.size.y * 0.5f);
        }
    }

    void Update()
    {
        if (joystick == null) return;
        // Movement vectors that the joystick affects
        Vector2 movement = joystick.GetAxis();
        transform.Translate(new Vector3(movement.x, 0, 0) * speed * Time.deltaTime);
        GetComponent<Animator>().SetBool("isWalking", movement.x != 0);
        if (bendToggle.isOn && IsJumpHeld())
        {
            Bend();
        }
    }
    void Interact()
    {
        GetComponent<Animator>().SetTrigger("Interact");
        Debug.Log("Player interacted!");
    }

}
