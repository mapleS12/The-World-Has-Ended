using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen instance;

    public Image fadeImage;
    public float fadeTime = 1f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static void FadeOut()
    {
        if (instance != null)
            instance.StartCoroutine(instance.FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        fadeImage.raycastTarget = true;

        Color c = fadeImage.color;
        c.a = 0;
        fadeImage.color = c;

        while (c.a < 1)
        {
            c.a += Time.deltaTime / fadeTime;
            fadeImage.color = c;
            yield return null;
        }
    }
}
