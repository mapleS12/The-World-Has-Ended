using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private Inventory inventory;
    public Camera interactionCamera; // testing alternative to Camera.main

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void TryInteract_NewInput()
    {
        //Debug.Log("Topmost UI: " + UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject());

        if (Camera.main == null)
        {
            Debug.LogError("PlayerInteraction: No main camera found.");
            return;
        }

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
        Vector3 worldPos = interactionCamera.ScreenToWorldPoint(screenPos); // testing alternative to Camera.main, to solve bug.
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
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