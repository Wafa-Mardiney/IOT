using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalSpawner : MonoBehaviour
{
    public bool startSpawning = false;
    public GameObject signalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void initSpawn()
    //{
    //    startSpawning = true;
    //    StartCoroutine(spawnSignal());
    //}

    //IEnumerator spawnSignal()
    //{
    //    while(startSpawning)
    //    {
    //        yield return new WaitForSeconds(1);
    //        GameObject temp = Instantiate(signalPrefab,this.transform);
    //        signalPrefab.GetComponent<ParabolaController>().ParabolaRoot = SceneManager.Instance.allRoots[Random.Range(0, SceneManager.Instance.allRoots.Length)];
    //        signalPrefab.GetComponent<ParabolaController>().FollowParabola();
    //    }
    //}
}
