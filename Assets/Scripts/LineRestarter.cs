using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRestarter : MonoBehaviour
{
    public CurvedLineRenderer[] allLines;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        allLines = FindObjectsOfType<CurvedLineRenderer>();
        for (int i = 0; i < allLines.Length; i++)
        {
            allLines[i].gameObject.SetActive(false);
        }
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
