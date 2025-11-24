using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public void NewGameButton()
    {
        SceneManager.LoadScene("NewGame"); // Load the scene with index 1
    }

    public void LoadGameButton()
    {
        SceneManager.LoadScene("LoadGame"); // Load the scene with index 2
    }

    // Function to exit the game.
    public void ExitButton()
    {
        Debug.Log("Quit");    // Log quit message to console
        Application.Quit();   // Quit the application
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings"); // Load the scene with index 3
    }

    public void AccomplishmentsButton()
    {
        SceneManager.LoadScene("Accomplishments"); // Load the scene with index 4
    }
}
