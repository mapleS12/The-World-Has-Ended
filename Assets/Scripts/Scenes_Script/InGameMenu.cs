using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject InGameMenuPanel;
    public GameObject ReturnToGamePanel;
    public GameObject SaveGamePanel;
    //public GameObject ReturnToMainMenuPanel;
    public GameObject SettingsPanel;
    
    // --- Panel Transitions ---

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

    public void OpenRetirnToGamePanel()
    {
        //SceneManager.LoadScene()
        //InGameMenuPanel.SetActive(false);
        //ReturnToGamePanel.SetActive(true);
    }

    // Place SaveGamePanel code here when implemented  

    public void ReturnToMainMenuPanel()
    {
        // Load the new game scene which is the MainMenu panel (scene 0)
        SceneManager.LoadScene(0);
    }
}