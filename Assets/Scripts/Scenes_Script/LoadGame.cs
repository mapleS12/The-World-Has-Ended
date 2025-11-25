using UnityEngine;

public class LoadGamePanel : MonoBehaviour
{
    public void LoadFromSlot(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
