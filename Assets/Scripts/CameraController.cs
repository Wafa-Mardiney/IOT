using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Animator cameraAnimator;
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
        cameraAnimator = GetComponent<Animator>();
    }

    public void startNextAnimation()
    {
        cameraAnimator.SetTrigger("Next");
    }
}
