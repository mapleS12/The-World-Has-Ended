using UnityEngine;
using System.IO;


public class SaveManager : MonoBehaviour
{
    // Static instance to allow easy acces from any other script.
    public static SaveManager Instance;

    private string saveFileName = "playerdata.json";

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- SAVE FUNCTION ---
    public void SaveGame()
    {
        // 1. Create a new GameData object
        GameData data = new GameData();

        // 2. Fill the data object with current game state

        // 3. Convre the GameData object to a JSON string
        string json = JsonUtility.ToJson(data, true);

        // 4. Determine the file path
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        // 5. Write the JSON string to the file
        File.WriteAllText(filePath, json);

        Debug.Log("Game Saved to " + filePath);
    }

    // --- LOAD FUNCTION ---
    public void LoadGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            // 1. Read the JSON string from the file
            string json = File.ReadAllText(filePath);

            // 2. Convert the JSON string back to a GameData object
            GameData data = JsonUtility.FromJson<GameData>(json);

            // 3. Apply the loaded data to the game state

            // Load the correct scene first
            UnityEngine.SceneManagement.SceneManager.LoadScene(data.currentSceneIndex);

            Debug.Log("Game Loaded from " + filePath);
        }
        else
        {
            Debug.LogWarning("Save file not found at " + filePath);
        }
    }
}
