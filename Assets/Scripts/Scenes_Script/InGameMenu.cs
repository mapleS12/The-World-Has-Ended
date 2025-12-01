using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject InGameMenuPanel;
    public GameObject ReturnToGamePanel;
    public GameObject SaveGamePanel;
    public GameObject SettingsPanel;
    public GameObject AccomplishmentsPanel;
    public GameObject MapPanel;
    public GameObject InventoryPanel;
    public GameObject QuestsPanel;
    public GameObject DimmingOverlay; //
    
    // --- Panel Transitions ---

    // Call when menu button in active game is pressed.
    public void OpenInGameMenu()
    {
        Time.timeScale = 0f; // Pauses game
        InGameMenuPanel.SetActive(true);
        DimmingOverlay.SetActive(true);
    }

    public void OpenSettingsPanel()
    {
        InGameMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

        public void ReturnFromSettings()
    {
        SettingsPanel.SetActive(false);
        InGameMenuPanel.SetActive(true);
    }

    public void OpenAccomplishmentsPanel()
    {
        InGameMenuPanel.SetActive(false);
        AccomplishmentsPanel.SetActive(true);
    }

    public void ReturnFromAccomplishments()
    {
        AccomplishmentsPanel.SetActive(false);
        InGameMenuPanel.SetActive(true);
    }

    public void OpenMapPanel()
    {
        InGameMenuPanel.SetActive(false);
        MapPanel.SetActive(true);
    }

    public void ReturnFromMap()
    {
        MapPanel.SetActive(false);
        InGameMenuPanel.SetActive(true);
    }

    public void OpenInventoryPanel()
    {
        InGameMenuPanel.SetActive(false);
        InventoryPanel.SetActive(true);
    }

    public void ReturnFromInventory()
    {
        InventoryPanel.SetActive(false);
        InGameMenuPanel.SetActive(true);
    }

    public void OpenQuestsPanel()
    {
        InGameMenuPanel.SetActive(false);
        QuestsPanel.SetActive(true);
    }

    public void ReturnFromQuests()
    {
        QuestsPanel.SetActive(false);
        InGameMenuPanel.SetActive(true);
    }

    // --- SPECIAL PANELS ---
    
    public void OpenReturnToGamePanel()
    {
        InGameMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        AccomplishmentsPanel.SetActive(false);
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        QuestsPanel.SetActive(false);
        DimmingOverlay.SetActive(false);

        Time.timeScale = 1f; // Resumes game    
    }

    public void ClickedSaveGamePanel()
    {
        // Implement save game logic here
        Debug.Log("Game Saved!");
    }

    // Place SaveGamePanel code here when implemented  

    public void ReturnToMainMenuPanel()
    {
        Time.timeScale = 1f;
        
        // Load the new game scene which is the MainMenu panel (scene 1)
        SceneManager.LoadScene(1);

        Destroy(transform.root.gameObject); // Clean up InGameMenu object
    }
}