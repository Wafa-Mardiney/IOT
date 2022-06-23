using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WEbCamTest : MonoBehaviour
{
    Renderer temp;
    RenderTexture camTex;
    // Start is called before the first frame update
    void Start()
    {
        WebCamTexture cam = new WebCamTexture();
        temp = GetComponent<Renderer>();
        temp.material.mainTexture = cam;
        cam.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
