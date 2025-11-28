using UnityEngine;
using UnityEngine.UI;
using Terresquall;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public VirtualJoystick joystick;
    public float jumpForce = 10f;
    public HoldButton jumpHoldButton;
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
        if (joystick == null) return;
        
        Vector2 movement = joystick.GetAxis();
        
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
        anim.SetBool("isWalking", movement.magnitude > 0.01f);
        
        if (jumpHoldButton != null && jumpHoldButton.isPressed && IsGrounded())
            Jump();
    }
    
    bool IsGrounded()
    {
        return Mathf.Abs(rb.velocity.y) < 0.01f;
    }
    
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
    }
    
    void Interact()
    {
        anim.SetTrigger("Interact");
        if (playerInteraction != null)
            playerInteraction.TryInteract();
        Debug.Log("Player interacted!");
    }
}