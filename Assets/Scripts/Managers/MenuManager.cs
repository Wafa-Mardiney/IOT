using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_Text message, headline;
    [TextArea]
    public string[] Slide1Msgs, slide2Msg, slide3Msg;
    [TextArea]
    public string slide3Headline, slide4Headline;
    public int currentMsg = 0;
    public GameObject clickingHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
