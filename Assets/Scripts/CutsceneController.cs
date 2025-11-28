
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [System.Serializable]
    public class Slide
    {
        public Sprite Image;
        [TextArea(3, 10)] public string Text;
        public float Duration = 3f;
    }

    public Image cutsceneImage;         
    public TMP_Text cutsceneText;         
    public float fadeTime = 0.8f;         
    public Slide[] slides;              

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            yield return StartCoroutine(ShowSlide(slides[i]));
        }


        SceneManager.LoadScene("Tutorial");
    }

    IEnumerator ShowSlide(Slide slide)
    {

        cutsceneImage.sprite = slide.Image;
        cutsceneText.text = slide.Text;

        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(slide.Duration);

        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float start, float end)
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, t / fadeTime);

            // Fade image
            Color c = cutsceneImage.color;
            c.a = alpha;
            cutsceneImage.color = c;

            // Fade text
            Color tcol = cutsceneText.color;
            tcol.a = alpha;
            cutsceneText.color = tcol;

            yield return null;
        }
    }
}
