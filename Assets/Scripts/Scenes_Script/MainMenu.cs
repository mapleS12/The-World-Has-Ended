using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script will be attached to a persistent object in your Main Menu Scene (e.g., a 'GameManager' or 'Canvas' object)
public class MainMenu : MonoBehaviour
{
    // Public GameObject variables to hold references to the UI Panels (set these in the Unity Inspector)
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject AccomplishmentsPanel;

    // --- SCENE LOADING FUNCTIONS (Used for major transitions) ---

    public void NewGameButton()
    {
        // IMPORTANT: Ensure "NewGame" is added to your Build Settings (File -> Build Settings)
        SceneManager.LoadScene("NewGame");
    }

    public void LoadGameButton()
    {
        // IMPORTANT: Ensure "LoadGame" is added to your Build Settings
        SceneManager.LoadScene("LoadGame");
    }

    // --- PANEL TOGGLE FUNCTIONS (Used for UI overlays) ---

    // Function to open the Settings Panel
    public void OpenSettings()
    {
        // 1. Hide the main menu
        MainMenuPanel.SetActive(false);
        // 2. Show the settings panel
        SettingsPanel.SetActive(true);
    }

    // Function to open the Accomplishments Panel
    public void OpenAccomplishments()
    {
        // 1. Hide the main menu
        MainMenuPanel.SetActive(false);
        // 2. Show the accomplishments panel
        AccomplishmentsPanel.SetActive(true);
    }

    // Function to exit the game.
    public void ExitButton()
    {
        Debug.Log("Quit"); // Log quit message to console
        Application.Quit(); // Quit the application (only works in a built game)
    }
}

// This script will be attached to the SettingsPanel and AccomplishmentsPanel
// It handles the "Back" functionality to return to the main menu.
public class UIPanelController : MonoBehaviour
{
    // Public GameObject variables to hold references (set these in the Unity Inspector)
    public GameObject PanelToClose; // e.g., SettingsPanel or AccomplishmentsPanel
    public GameObject PanelToOpen;  // e.g., MainMenuPanel

    // Function called by the "Back" button on a panel
    public void ClosePanel()
    {
        // 1. Hide the current panel
        PanelToClose.SetActive(false);
        // 2. Show the panel we want to return to
        PanelToOpen.SetActive(true);
    }
}