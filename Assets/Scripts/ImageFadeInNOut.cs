using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInNOut : MonoBehaviour
{
    public float time;
    public float fadeLimit;
    void Update()
    {
    }

    public void fadeIn()
    {
        StartCoroutine(FadeImageToFullAlpha(time, this.GetComponent<Image>()));
    }

    public void fadeOut()
    {
        StartCoroutine(FadeImageToZeroAlpha(time, this.GetComponent<Image>()));
    }

    public IEnumerator FadeImageToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < fadeLimit)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeImageToZeroAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
