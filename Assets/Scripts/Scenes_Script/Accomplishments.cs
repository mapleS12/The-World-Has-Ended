using UnityEngine;

public class AccomplishmentsScreen : MonoBehaviour
{
    public GameObject RegionCompletion;
    public GameObject CollectibleCompletion;
    public GameObject QuestsCompletion;
    
    public void RefreshAccomplishments()
    {
        // Implement logic to refresh/show accomplishments
        Debug.Log("Accomplishments refreshed.");
    }

    public void OpenRegion()
    {
        RegionCompletion.SetActive(true);
        CollectibleCompletion.SetActive(false);
        QuestsCompletion.SetActive(false);
    }

    public void OpenCollectible()
    {
        RegionCompletion.SetActive(false);
        CollectibleCompletion.SetActive(true);
        QuestsCompletion.SetActive(false);
    }

    public void OpenQuests()
    {
        RegionCompletion.SetActive(false);
        CollectibleCompletion.SetActive(false);
        QuestsCompletion.SetActive(true);
    }
}