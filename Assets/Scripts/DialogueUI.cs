using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;         
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject continueIcon;   

    [Header("Settings")]
    public string speakerName = "Companion";
    public float typeSpeed = 0.03f;

    [Header("Raycast")]

    public Canvas uiCanvas;

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);

        if (continueIcon != null)
            continueIcon.SetActive(false);

        if (uiCanvas == null)
        {
            uiCanvas = FindFirstObjectByType<Canvas>();
        }

        if (uiCanvas != null)
            raycaster = uiCanvas.GetComponent<GraphicRaycaster>();

        eventSystem = EventSystem.current;

        if (raycaster == null)
            Debug.LogWarning("DialogueUI: No GraphicRaycaster found on Canvas. UI filtering may not work.");
        if (eventSystem == null)
            Debug.LogWarning("DialogueUI: No EventSystem found in scene.");
    }


    public IEnumerator Say(string text)
    {
        if (panel == null || nameText == null || dialogueText == null)
        {
            Debug.LogError("DialogueUI not wired correctly in Inspector!");
            yield break;
        }

        panel.SetActive(true);
        nameText.text = speakerName;
        dialogueText.text = "";
        if (continueIcon != null) continueIcon.SetActive(false);

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        if (continueIcon != null) continueIcon.SetActive(true);

        bool advanced = false;
        while (!advanced)
        {
            advanced = CheckAdvanceInput();
            yield return null;
        }

        if (continueIcon != null) continueIcon.SetActive(false);
    }


    bool CheckAdvanceInput()
    {
        Vector2 screenPos;
        //bool pressed = false;

        if (Touchscreen.current != null)
        {
            var t = Touchscreen.current.primaryTouch;
            if (t.press.wasPressedThisFrame)
            {
                screenPos = t.position.ReadValue();
                //pressed = true;

                if (IsOverNonDialogueUI(screenPos))
                    return false;       

                return true;            
            }
        }


        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            screenPos = Mouse.current.position.ReadValue();
           // pressed = true;

            if (IsOverNonDialogueUI(screenPos))
                return false;

            return true;
        }

        return false;
    }


    bool IsOverNonDialogueUI(Vector2 screenPos)
    {
        if (raycaster == null || eventSystem == null)
            return false; 

        PointerEventData data = new PointerEventData(eventSystem);
        data.position = screenPos;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);

        if (results.Count == 0)
            return false; 

        foreach (var r in results)
        {
            GameObject hit = r.gameObject;
            if (hit == null) continue;

            if (panel != null && hit.transform.IsChildOf(panel.transform))
                continue;

            if (hit.GetComponent<Button>() != null ||
                hit.GetComponent<Toggle>() != null ||
                hit.GetComponent<Slider>() != null ||
                hit.GetComponent<Scrollbar>() != null)
            {

                return true;
            }
        }

        return false;
    }
}
