using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicTest : MonoBehaviour
{

    private RawImage rawImage;
    bool moveToFront = false;
    Canvas canvas;
    Vector3 newPos;
    WebCamTexture webcamTexture;
    void Start()
    {
        webcamTexture = new WebCamTexture();
        rawImage = GetComponent<RawImage>();
        rawImage.texture = webcamTexture;
        rawImage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
        canvas = GetComponentInParent<Canvas>();
        StartCoroutine(stopCameraWithDelay());
    }

    IEnumerator stopCameraWithDelay()
    {
        yield return new WaitForSeconds(4);
        webcamTexture.Stop();
        //SceneController.Instance.picController.welcomeMsg.SetActive(true);
        yield return new WaitForSeconds(2);
        //SceneController.Instance.menuController.enableTutorialScreen();
        //SceneManager.Instance.cameraController.startNextAnimation();
        SceneManager.Instance.startExperience();
    }

    private void Update()
    {
        if (moveToFront)
        {
            canvas.transform.position = Vector3.Lerp(canvas.transform.position, newPos, 2f * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        canvas = GetComponentInParent<Canvas>();
        newPos = new Vector3(canvas.transform.position.x, canvas.transform.position.y, 2.2f);
        moveToFront = true;
    }
}
