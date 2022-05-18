using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARController : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        startPlayerActivities();
    }

    private void startPlayerActivities()
    {
        
    }
}
