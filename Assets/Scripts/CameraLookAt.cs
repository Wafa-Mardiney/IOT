using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using arfound

public class CameraLookAt : MonoBehaviour
{
    public Transform lookAtCamera;
    //public ARCamera arCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtCamera);
    }
}
