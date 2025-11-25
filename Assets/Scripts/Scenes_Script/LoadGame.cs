using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public void LoadFromSlot(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
