using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public CompanionAI companion;
    public roPlayerController player;

    [Header("Objects")]
    public EnvironmentObject broom;
    public EnvironmentObject experimentLog;
    public EnvironmentObject[] trashObjects;
    public GameObject exitDoorTrigger;

    [Header("UI")]
    public GameObject mapButton;
    public DialogueUI dialogue;

    private bool exited = false;

    private void Start()
    {
        mapButton.SetActive(false);
        exitDoorTrigger.SetActive(false);

        StartCoroutine(TutorialFlow());
    }

    IEnumerator TutorialFlow()
    {
        Debug.Log("TUTORIAL STARTED");

        // WAKE UP
        yield return dialogue.Say("Oh good, I'm glad you're awake — can you move?");
        yield return dialogue.Say("Use the joystick to walk.");

        Vector3 startPos = player.transform.position;

        // Wait until position changes
        while (Vector3.Distance(startPos, player.transform.position) < 0.8f)
            yield return null;

        yield return dialogue.Say("Looks like your muscles still work.");


        // PICK UP BROOM
        yield return dialogue.Say("This place is falling apart… we need something to clear the waste near the entrance.");
        yield return dialogue.Say("Find the broom and click on it to collect.");
        Highlight(broom);

        // Wait for destruction
        while (broom != null && broom.gameObject != null)
            yield return null;

        RemoveHighlight(broom);
        yield return dialogue.Say("You found the KALILI COORP DISPOSAL UNIT!");
        yield return dialogue.Say("[Use this tool to sweep up trash!]");

        // PICK UP EXPERIMENT LOG
        yield return dialogue.Say("It looks like there is a data log over there, lets check it out so we get more information.");
        Highlight(experimentLog);

        while (experimentLog != null && experimentLog.gameObject != null)
            yield return null;

        RemoveHighlight(experimentLog);
        
        yield return dialogue.Say("[Dusty research notes mentioning sister facilities across various regions.]");
        yield return dialogue.Say("[Scattered coordinates and half-erases names suggesting a direction, but nothing you can reliably follow... yet.]");
        yield return dialogue.Say("We lets save this information for later and focus on getting out.");

        // CLEAN TRASH
        yield return dialogue.Say("Lets use the KALILI COORP DISPOSAL UNIT to clean this up.");
        yield return dialogue.Say("[Tap trash to clean.]");

        bool trashLeft = true;

        while (trashLeft)
        {
            trashLeft = false;

            foreach (var t in trashObjects)
            {
                if (t != null && t.gameObject != null)
                {
                    trashLeft = true;
                    break;
                }
            }

            yield return null;
        }

        yield return dialogue.Say("Good job!");


        yield return dialogue.Say("Ready to go? Let's get out now that the path is cleared.");
        yield return dialogue.Say("Ive unlocked the doors for us to leave!");


        // EXIT

        exitDoorTrigger.SetActive(true);
        exited = false;

        ExitTrigger.OnPlayerExit += ExitCallback;

        while (!exited)
            yield return null;

        ExitTrigger.OnPlayerExit -= ExitCallback;

        FadeScreen.FadeOut();
        yield return new WaitForSeconds(1f);

        SceneLoader.Load("Level1");
    }


    private void MapOpenedCallback()
    {
        mapOpened = true;
        Debug.Log("Map opened!");
    }

    private void ExitCallback()
    {
        exited = true;
        Debug.Log("Exit reached!");
    }


    void Highlight(EnvironmentObject eo)
    {
        if (eo == null || eo.gameObject == null) return;
        var sr = eo.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = new Color(1f, 1f, 0.5f);
    }

    void RemoveHighlight(EnvironmentObject eo)
    {
        if (eo == null || eo.gameObject == null) return;
        var sr = eo.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.white;
    }
}
