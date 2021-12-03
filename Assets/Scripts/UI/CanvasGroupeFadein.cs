using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupeFadein : MonoBehaviour
{

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float time;
    public void FadeIn()
    {
        canvasGroup.alpha = 0;
        StartCoroutine(FadeInCGroup());
    }

    IEnumerator FadeInCGroup()
    {
        float tempTime = 0;

        while (tempTime <= time)
        {
            tempTime += Time.deltaTime;
            canvasGroup.alpha = tempTime / time;
            yield return new WaitForEndOfFrame();
        }
        canvasGroup.alpha = 1;
    }

}
