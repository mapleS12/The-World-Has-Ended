using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject AccomplishmentsPanel;
    public GameObject NewGamePanel;
    public GameObject LoadGamePanel;
    
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
        MainMenuPanel.SetActive(false);
        NewGamePanel.SetActive(true);
    }

    public void OpenLoadGamePanel()
    {
        MainMenuPanel.SetActive(false);
        LoadGamePanel.SetActive(true);
    }

    // --- SCENE LOADING ---
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadSavedGame(string savedGameSceneName)
    {
        SceneManager.LoadScene(savedGameSceneName); // E.g., SceneManager.LoadScene("Level1")
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}