using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    CanvasGroup panel;
    public static FadeToBlack instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        panel = GetComponent<CanvasGroup>();
    }
    public void Fade(float fadeSpeed, float fadeDuration)
    {
        StartCoroutine(FadeIn(fadeSpeed));
        StartCoroutine(FadeOut(fadeSpeed, fadeDuration));
    }
    IEnumerator FadeIn(float fadeSpeed)
    {
        while (panel.alpha < 1)
        {
            panel.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
    IEnumerator FadeOut(float fadeSpeed, float fadeDuration)
    {
        if(fadeDuration >= 999)
        {
            yield break;
        }
        yield return new WaitForSeconds(fadeDuration);
        while (panel.alpha > 0)
        {
            panel.alpha -= Time.deltaTime * fadeSpeed;
            yield return null; 
        }
    }
}
