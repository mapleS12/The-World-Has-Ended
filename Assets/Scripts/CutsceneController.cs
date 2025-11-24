using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject player;
    public TutorialManager tutorial;

    void Start()
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        director.stopped += OnCutsceneEnd;
    }

    void OnCutsceneEnd(PlayableDirector obj)
    {
        player.GetComponent<PlayerMovement>().enabled = true;

        tutorial.StartTutorial();
    }
}
