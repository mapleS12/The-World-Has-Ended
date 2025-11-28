using UnityEngine;
using Terresquall;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class roPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public int joystickID = 5;

    private Rigidbody2D rb;
    private Vector2 input;

    private PlayerInteraction interactionSystem;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        interactionSystem = GetComponent<PlayerInteraction>();

        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    void Update()
    {
        // Read the joystick with ID = 5
        VirtualJoystick js = VirtualJoystick.GetInstance(joystickID);
        input = js != null ? js.GetAxis() : Vector2.zero;

        // Tap to interact
        if (WasTapped())
            interactionSystem.TryInteract_NewInput();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }

    bool WasTapped()
    {
        // Mobile touch
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            return touch.press.wasPressedThisFrame;
        }

        // Editor mouse
        if (Mouse.current != null)
            return Mouse.current.leftButton.wasPressedThisFrame;

        return false;
    }
}
