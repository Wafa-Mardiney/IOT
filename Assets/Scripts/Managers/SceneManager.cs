using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public MenuManager menuManager;
    public int currentMsgIndex = 0;
    public CameraController cameraController;
    public Transform[] allSignalPoints;
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
        startExperience();
    }

    public void startExperience()
    {
        StartCoroutine(experienceWithTime());
    }

    IEnumerator experienceWithTime()
    {
        yield return new WaitForSeconds(1); ;
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
}
