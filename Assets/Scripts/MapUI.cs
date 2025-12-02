using UnityEngine;

public class MapUI : MonoBehaviour
{
    public static System.Action OnMapOpened;

    public GameObject mapPanel;

    public void OpenMap()
    {
        mapPanel.SetActive(true);
        OnMapOpened?.Invoke(); 
        Debug.Log("MAP OPENED EVENT FIRED");
    }

    public void CloseMap()
    {
        mapPanel.SetActive(false);
    }
}
