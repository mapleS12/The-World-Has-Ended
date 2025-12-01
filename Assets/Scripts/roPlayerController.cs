using UnityEngine;
using Terresquall;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class roPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public int joystickID = 5;

    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 facing = Vector2.down; 

    private Animator anim;
    private SpriteRenderer sr;

    private PlayerInteraction interactionSystem;

    // animation
    private string currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        interactionSystem = GetComponent<PlayerInteraction>();

        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Read joystick with ID = 5
        VirtualJoystick js = VirtualJoystick.GetInstance(joystickID);
        input = js != null ? js.GetAxis() : Vector2.zero;

        bool tapped = WasTapped();
        if (tapped)
        {
            interactionSystem.TryInteract_NewInput();
            PlayInteractAnimation();
        }

        //transform.localScale = new Vector3(4f, 4f, 1f);
        //sr.size = new Vector2(4f, 4f); 
        UpdateDirection();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        float speed = GetMovementSpeed();
        rb.linearVelocity = input * speed;
    }

    //run or movement speed
    float GetMovementSpeed()
    {
        float mag = input.magnitude;

        // Run if user pulls joystick far enough
        if (mag > 0.5f)
            return runSpeed;

        return walkSpeed;
    }

    // interact animation
    float interactTimer = 0f;
    public float interactDuration = 0.35f;

    void PlayInteractAnimation()
    {
        interactTimer = interactDuration;
    }

    // directional movement
    void UpdateDirection()
    {
        if (input.sqrMagnitude > 0.001f)
            facing = input;
    }

    string GetDirectionSuffix()
    {
        if (Mathf.Abs(facing.y) > Mathf.Abs(facing.x))
            return facing.y > 0 ? "Up" : "Down";
        else
            return "Side";
    }

    //animation state
    void UpdateAnimation()
    {
        string dir = GetDirectionSuffix();
        string newState;

        if (interactTimer > 0f)
        {
            interactTimer -= Time.deltaTime;
            newState = "Interact_" + dir;
        }
        else if (input.sqrMagnitude < 0.01f)
        {
            newState = "Idle_" + dir;
        }
        else
        {
            bool isRun = input.magnitude > 0.8f;
            newState = (isRun ? "Run_" : "Walk_") + dir;
        }

        // Flip sprite for left/right
        if (dir == "Side")
        {
            if (facing.x < -0.1f) sr.flipX = true;
            if (facing.x > 0.1f) sr.flipX = false;
        }

        ChangeAnimationState(newState);
    }

    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;

        currentState = newState;
        anim.CrossFade(newState, 0.1f);
    }

    // tap detection
    bool WasTapped()
    {
        // mobile touch
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            return touch.press.wasPressedThisFrame;
        }

        // mouse click
        if (Mouse.current != null)
            return Mouse.current.leftButton.wasPressedThisFrame;

        return false;
    }
}