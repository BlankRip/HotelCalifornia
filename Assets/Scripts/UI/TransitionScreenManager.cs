using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreenManager : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    Color color;


    public void FadeIn(float t1)
    {
        StartCoroutine(FadeInCoroutine(t1));
    }

    public void FadeOut(float t1)
    {
        StartCoroutine(FadeOutCoroutine(t1));
    }


    IEnumerator FadeOutCoroutine(float t1)
    {
        float recValue = t1;

        while (t1 > 0)
        {
            t1 -= Time.deltaTime;
            color = fadeImage.color;
            color.a = t1 / recValue;
            fadeImage.color = color;
            yield return new WaitForEndOfFrame();
        }
        fadeImage.raycastTarget = false;

    }

    IEnumerator FadeInCoroutine(float t1)
    {
        float value = 0;
        fadeImage.raycastTarget = true;

        while (value < t1)
        {
            value += Time.deltaTime;
            color = fadeImage.color;
            color.a = value / t1;
            fadeImage.color = color;
            yield return new WaitForEndOfFrame();
        }


    }


}
