using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSignals : MonoBehaviour
{
    public Vector3 destination;
    public float moveSpeed;
    bool StartMoving = false;
    bool searchingDestination = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (searchingDestination)
        {
            return; 
        }
        if (StartMoving)
        {
             
        }
    }

    public void getDestination()
    {
        destination = SceneManager.Instance.allSignalPoints[Random.Range(0, SceneManager.Instance.allSignalPoints.Length)].position;
    }
}
