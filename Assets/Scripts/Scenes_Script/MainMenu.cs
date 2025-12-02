using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject AccomplishmentsPanel;
    public GameObject NewGamePanel;
    public GameObject LoadGamePanel;

    [Header("Canvas Root (VERY IMPORTANT)")]
    public GameObject mainMenuCanvas; // ← assign your MAIN MENU CANVAS here

    // --- Panel Transitions ---
    public void OpenSettingsPanel()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void ReturnFromSettings()
    {
        SettingsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void OpenAccomplishmentsPanel()
    {
        MainMenuPanel.SetActive(false);
        AccomplishmentsPanel.SetActive(true);
    }

    public void ReturnFromAccomplishments()
    {
        AccomplishmentsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void OpenNewGamePanel()
    {
        // 1. SAFELY disable menu canvas
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
            // or Destroy(mainMenuCanvas);  // if you NEVER need the menu again
        }
        else
        {
            Debug.LogWarning("mainMenuCanvas is NOT assigned! Raycasts will block the tutorial input.");
        }

        // 2. LOAD GAME
        SceneManager.LoadScene("Intro Cutscene");

    }

    public void OpenLoadGamePanel()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.LoadGame();
        }
        else
        {
            Debug.LogError("Cannot load game. SaveManager is not active.");
        }
    }

    // --- SCENE LOADING ---
    public void LoadSavedGame(string savedGameSceneName)
    {
        SceneManager.LoadScene(savedGameSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
