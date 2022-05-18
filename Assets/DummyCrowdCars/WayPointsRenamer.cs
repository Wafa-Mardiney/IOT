using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsRenamer : MonoBehaviour
{

    public string WpName = "WayPointNameHere";

    // Start is called before the first frame update
    void Start()
    {
        int counternum = 0;
        foreach (Transform child in transform) {
            child.gameObject.name = WpName + "-" + counternum;
            counternum++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
