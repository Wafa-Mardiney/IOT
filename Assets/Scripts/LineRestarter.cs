using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRestarter : MonoBehaviour
{
    public CurvedLineRenderer[] allLines;
    private void OnEnable()
    {
        allLines = FindObjectsOfType<CurvedLineRenderer>();
        for (int i = 0; i < allLines.Length; i++)
            allLines[i].gameObject.SetActive(false);
        StartCoroutine(startLineAgain());
    }

    IEnumerator startLineAgain()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < allLines.Length; i++)
        {
            allLines[i].gameObject.SetActive(true);
        }
    }
}
