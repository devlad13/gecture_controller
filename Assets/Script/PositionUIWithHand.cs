using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System;
using Mediapipe.Unity.PoseTracking;

public class PositionUIWithHand : MonoBehaviour
{
    public RectTransform uiElement; // Reference to the UI element to position
    //public UdpSocket _udpSocket; // udp socket script reference
    public Camera menuCamera; // menuCamera Reference
    public float screenWidth; // Width of the screen
    public float screenHeight; // Height of the screen

    public Image radialFill;
    public float fillSpeed = 2f;
    private float progress = 0f;

    // Set a limit to the number of hits to check per frame
    public int hitLimit = 100;
    public int hitCount = 0;
    float mouseOverTime = 0f;
    bool performAction = false;
    bool clicked = false;
    //public EffectSoundManager _effectSound;

    void Start()
    {
        // Create a new instance of the UdpSocket class
        //_udpSocket = FindObjectOfType<UdpSocket>().GetComponent<UdpSocket>();

        // Get the width and height of the primary screen
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        progress = 0f;
        mouseOverTime = 0f;
        performAction = false;
    }

    /* private void UpdateProgress(Button button)
     {
         progress += Time.deltaTime * fillSpeed;
         progress = Mathf.Clamp01(progress);
         radialFill.fillAmount = progress;
         if (progress >= 1f)
         {
             clicked = true;
             button.onClick.Invoke();
             mouseOverTime = 0f;
             performAction = false;
             ResetProgress();
             CancelInvoke("UpdateProgress");
         }
     }*/


    void Update()
    {
        // Make sure the _udpSocket object is not null before using it

        // Get the position of the user's hand in world space
        //Vector3 handPosition = new Vector3(_udpSocket.mouseCursorX, _udpSocket.mouseCursorY, 1f);
        Vector3 handPosition = new Vector3(-(float)BodyLandMarks.right_wristx, -(float)BodyLandMarks.right_wristy, 1f);
        // Convert the world position to viewport space
        Vector3 viewportPosition = menuCamera != null ? menuCamera.WorldToViewportPoint(handPosition) : Camera.main.WorldToViewportPoint(handPosition); //uiCamera.WorldToViewportPoint(handPosition);

        // Invert the x and y values of the viewport position to flip the movement of the UI element
        viewportPosition.x = 1f - viewportPosition.x;
        viewportPosition.y = 1f - viewportPosition.y;

        // Clamp the viewport position to the visible screen area
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0f, 1f);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0f, 1f);

        // Set the position of the UI element relative to the hand position
        uiElement.position = menuCamera != null ? menuCamera.ViewportToScreenPoint(viewportPosition) : Camera.main.ViewportToScreenPoint(viewportPosition); //menuCamera.ViewportToScreenPoint(viewportPosition);
        uiElement.eulerAngles = new Vector3(0, 0, 0);
        //sahil
        /*Ray ray = menuCamera != null ? menuCamera.ViewportPointToRay(viewportPosition) : Camera.main.ViewportPointToRay(viewportPosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.green);

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = uiElement.position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);


        *//* // Get the position of the user's hand in world space
         Vector3 handPosition = new Vector3(_udpSocket.mouseCursorX, _udpSocket.mouseCursorY, 1f);

         // Convert the world position to screen space
         Vector2 screenPosition = menuCamera != null ? menuCamera.WorldToScreenPoint(handPosition) : Camera.main.WorldToScreenPoint(handPosition);

         // Invert the x and y values of the screen position to flip the movement of the UI element
         screenPosition.x = screenWidth - screenPosition.x;
         screenPosition.y = screenHeight - screenPosition.y;

         // Clamp the screen position to the visible screen area
         screenPosition.x = Mathf.Clamp(screenPosition.x, 0f, screenWidth);
         screenPosition.y = Mathf.Clamp(screenPosition.y, 0f, screenHeight);

         // Set the position of the UI element relative to the hand position
         uiElement.position = screenPosition;
         uiElement.eulerAngles = new Vector3(0, 0, 0);

         Ray ray = menuCamera != null ? menuCamera.ScreenPointToRay(screenPosition) : Camera.main.ScreenPointToRay(screenPosition);

         Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.green);

         PointerEventData eventData = new PointerEventData(EventSystem.current);
         eventData.position = screenPosition;

         List<RaycastResult> results = new List<RaycastResult>();
         EventSystem.current.RaycastAll(eventData, results);*//*

        foreach (RaycastResult result in results)
        {
            if (results.Count == 0)
            {
                mouseOverTime = 0f;
                performAction = false;
                ResetProgress();
                CancelInvoke("UpdateProgress");
                return;
            }  

            if (result.gameObject.GetComponent<Button>() != null)
            {
                GameObject hitObject = result.gameObject;
                Debug.Log("hitObject" + hitObject);

                if (hitObject.CompareTag("MusicSilderPlus"))
                {
                    Button button = hitObject.GetComponent<Button>();
                    button.onClick.Invoke();
                }
                if (hitObject.CompareTag("MusicSliderMinus"))
                {
                    Button button = hitObject.GetComponent<Button>();
                    button.onClick.Invoke();

                }
                if (hitObject.CompareTag("SoundSliderMinus"))
                {
                    Button button = hitObject.GetComponent<Button>();
                    button.onClick.Invoke();

                }
                if (hitObject.CompareTag("SoundSliderPlus"))
                {
                    Button button = hitObject.GetComponent<Button>();
                    button.onClick.Invoke();

                }
                else if (hitObject.CompareTag("TutorialPlay"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                        clicked = false;
                    }
                }
                else if (hitObject.CompareTag("PlayGame"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("TapToStart"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Option"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Quit"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("List"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("YesButton"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("NoButton"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Sound"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("MusicSlider"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Slider slider = result.gameObject.GetComponent<Slider>();
                        float currentValue = slider.value;
                        float newValue = (currentValue + 0.1f) % 1f; // Increment the value by 0.1 and wrap around if it goes over 1
                        slider.value = newValue;

                        Button button = hitObject.GetComponent<Button>();
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Music"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (result.gameObject.CompareTag("SoundSlider"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Slider slider = result.gameObject.GetComponent<Slider>();
                        float currentValue = slider.value;
                        float newValue = (currentValue + 0.1f) % 1f; // Increment the value by 0.1 and wrap around if it goes over 1
                        slider.value = newValue;

                        Button button = hitObject.GetComponent<Button>();
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("OptionDone"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("AvtarCancel"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Next"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Previous"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("AvatarDone"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Restart"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Home"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else if (hitObject.CompareTag("Tutorial"))
                {
                    mouseOverTime += Time.deltaTime;
                    if (mouseOverTime >= 3f && !performAction)
                    {
                        Button button = hitObject.GetComponent<Button>();
                        clicked = false;
                        StartCoroutine(ClickButtonRepeatedly(button, 1f, 3)); // Click the button 3 times with a 1-second delay between clicks
                    }
                }
                else
                {
                    mouseOverTime = 0f;
                    performAction = false;
                    ResetProgress();
                    CancelInvoke("UpdateProgress");
                }
            }
        }

}

public void ResetProgress()
{
    progress = 0f;
    radialFill.fillAmount = progress;
    CancelInvoke("UpdateProgress");
}

private IEnumerator ClickButtonRepeatedly(Button button, float delay, int numClicks)
{
    while (!clicked)
    {
        clicked = true;
        UpdateProgress(button);
        yield return new WaitForSeconds(delay);
        //button.onClick.Invoke();
        clicked = true;
        mouseOverTime = 0f;
        performAction = false;
        ResetProgress();
        CancelInvoke("UpdateProgress");
        StopCoroutine(ClickButtonRepeatedly(button, delay, numClicks));
    }
}*/
    }
}