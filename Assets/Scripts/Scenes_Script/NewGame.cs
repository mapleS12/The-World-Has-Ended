using UnityEngine;

public class NewGame : MonoBehaviour
{
    public void StartTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }
}
