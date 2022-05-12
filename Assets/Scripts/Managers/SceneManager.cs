using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public MenuManager menuManager;
    public int currentMsgIndex = 0;
    public CameraController cameraController;
    //public GameObject[] allSignals;
    public SignalSpawner signalSpawner;
    public bool sendingSignals = false;
    public GameObject[] allRoots;
    public GameObject signalPrefab;
    public float signalSpawnSpeed = 0.5f;
    public GameObject RootIconParent;
    public GameObject idleScreenAssets;
    public GameObject userLoginAssets;
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
        currentMsgIndex = 0;
        //startExperience();
        startLoginProcedure();
    }



    public void startLoginProcedure()
    {
        StartCoroutine(loginProcedure());
    }

    IEnumerator loginProcedure()
    {
        idleScreenAssets.SetActive(true);
        yield return new WaitForSeconds(3);
        idleScreenAssets.SetActive(false);
        userLoginAssets.SetActive(true);
        yield return new WaitForSeconds(3);
    }

    public void startExperience()
    {
        StartCoroutine(experienceWithTime());
    }

    IEnumerator experienceWithTime()
    {
        menuManager.message.text = "Welcome User!";
        menuManager.message.GetComponent<textFadeInNOut>().fadeIn();
        yield return new WaitForSeconds(6);
        menuManager.message.GetComponent<textFadeInNOut>().fadeOut();
        yield return new WaitForSeconds(3);
        userLoginAssets.SetActive(false);
        yield return new WaitForSeconds(1);
        cameraController.startNextAnimation();
        menuManager.message.text = menuManager.Slide1Msgs[currentMsgIndex];
        currentMsgIndex++;
        menuManager.message.GetComponent<textFadeInNOut>().fadeIn();
        yield return new WaitForSeconds(6);
        menuManager.message.GetComponent<textFadeInNOut>().fadeOut();
        yield return new WaitForSeconds(2);
        cameraController.startNextAnimation();
        menuManager.clickingHand.GetComponent<SlidesSlider>().showOnScreen();
        for (int i = 0; i < menuManager.slide2Msg.Length; i++)
        {
            menuManager.message.text = menuManager.slide2Msg[i];
            menuManager.message.GetComponent<textFadeInNOut>().fadeIn();
            yield return new WaitForSeconds(6);
            menuManager.message.GetComponent<textFadeInNOut>().fadeOut();
            yield return new WaitForSeconds(2);
        }
        menuManager.clickingHand.GetComponent<SlidesSlider>().removeFromScreen();
        cameraController.startNextAnimation();
        startSignals();
        RootIconParent.SetActive(true);
        menuManager.headline.text = menuManager.slide3Headline;
        menuManager.headline.GetComponent<textFadeInNOut>().fadeIn();
        yield return new WaitForSeconds(2);
        menuManager.message.GetComponent<SlidesSlider>().Reset();
        menuManager.message.GetComponent<textFadeInNOut>().fadeIn();
        yield return new WaitForSeconds(2);
        for (int i = 0; i < menuManager.slide3Msg.Length; i++)
        {
            menuManager.message.GetComponent<SlidesSlider>().Reset();
            menuManager.message.text = menuManager.slide3Msg[i];
            menuManager.message.GetComponent<SlidesSlider>().showOnScreen();
            yield return new WaitForSeconds(4);
            menuManager.message.GetComponent<SlidesSlider>().removeFromScreen();
            yield return new WaitForSeconds(2);
        }

    }

    public void startSignals()
    {
        sendingSignals = true;
        StartCoroutine(startSignalWithDelay());
    }

    IEnumerator startSignalWithDelay()
    {
        while (sendingSignals)
        {
            yield return new WaitForSeconds(signalSpawnSpeed);
            GameObject temp = Instantiate(signalPrefab, signalSpawner.transform);
            temp.GetComponent<ParabolaController>().FollowParabola();
        }
    }
}
