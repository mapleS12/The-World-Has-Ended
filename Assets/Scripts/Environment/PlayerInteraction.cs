using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void TryInteract_NewInput()
    {
        if (Camera.main == null) return;

        Vector2 screenPos;

        // --- Touch (Mobile) ---
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (!touch.press.isPressed) return; // no touch
            screenPos = touch.position.ReadValue();
        }
        else
        {
            // --- Mouse (Editor testing) ---
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                screenPos = Mouse.current.position.ReadValue();
            else
                return;
        }

        // SECOND SAFETY: avoid inf/nan
        if (float.IsInfinity(screenPos.x) || float.IsInfinity(screenPos.y))
            return;

        // Convert screen to world
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        Vector2 world = new Vector2(worldPos.x, worldPos.y);

        // Raycast directly at tapped position
        RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);

        if (hit.collider != null)
        {         
            if (hit.collider.TryGetComponent<EnvironmentObject>(out var envObj))
            {
                envObj.Interact(inventory);
            }
        }
    }
}