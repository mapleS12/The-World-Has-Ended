using UnityEngine;

// Attach this script to buttons or panels that handle "Back"
public class UIPanelController : MonoBehaviour
{
    // Assign from Inspector: PanelToClose = this panel, PanelToOpen = MainMenuPanel
    public GameObject PanelToClose;
    public GameObject PanelToOpen;

    public void ClosePanel()
    {
        PanelToClose.SetActive(false);
        PanelToOpen.SetActive(true);
    }
}
