using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public static System.Action OnPlayerExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<roPlayerController>())
        {
            OnPlayerExit?.Invoke();
        }
    }
}
