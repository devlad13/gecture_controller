using Mediapipe.Unity.PoseTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private float eventCooldown = 0f;
    float oldYValue = 0, jumpOrSwipeDiff = 0, oldXValue = 0, rightOrLeftDiff = 0, playerXvalue = 0, playerYvalue = 0;
    public bool pythonLeft,pythonRight,pythonDown,pythonUp, nextOperation;
    private Vector3 startPos, PosVec;
    float swipeThreshold = 1f;
    // Start is called before the first frame update
    void Start()
    {
        nextOperation = true;
        pythonLeft = pythonRight = pythonDown = pythonUp = false;
    }

    private void Update()
    {
        try
        {
           
                oldYValue = playerYvalue;
                oldXValue = playerXvalue;
                playerXvalue = BodyLandMarks.PlayerXvalue; //int.Parse(playerValueFromPython[0]);
                playerYvalue = BodyLandMarks.PlayerYvalue / 5;//int.Parse(playerValueFromPython[1]);
                Debug.Log("<color=green> old Position:: </color>" + playerXvalue + "++++" + playerYvalue);
                //Debug.Log("<color=green> new Position:: </color>" + playerXvalue + "++++" + playerYvalue);

                jumpOrSwipeDiff = (playerYvalue - oldYValue);
                rightOrLeftDiff = (playerXvalue - oldXValue);

                
                PosVec = new Vector3(playerXvalue, playerYvalue, 0);
                Vector3 swipeDelta = PosVec - startPos;

                
                if (rightOrLeftDiff > 20f && eventCooldown <= 0.5f)
                {
                    pythonRight = true;
                    pythonLeft = pythonDown = pythonUp = false;
                    eventCooldown = 1.5f;
                }
                else if (rightOrLeftDiff < -20f && eventCooldown <= 0.5f)
                {
                    pythonLeft = true;
                    pythonRight = pythonDown = pythonUp = false;
                    eventCooldown = 1.5f;
                }

                if (Mathf.Abs(swipeDelta.y) > swipeThreshold && Mathf.Abs(swipeDelta.x) < swipeThreshold)
                {
                    Debug.Log("<color=blue> Delta:: </color>" + rightOrLeftDiff + "++++" + jumpOrSwipeDiff);
                    if (jumpOrSwipeDiff > 1f && eventCooldown <= 0f)
                    {
                        Debug.Log("<color=red>UP Delta:: </color>" + rightOrLeftDiff + "++++" + jumpOrSwipeDiff);
                        pythonLeft = pythonRight = pythonDown = false;
                        pythonUp = true;
                        eventCooldown = 1f;
                    }
                    else if (jumpOrSwipeDiff < -1f && eventCooldown <= 0f)
                    {
                        Debug.Log("<color=red>DP Delta:: </color>" + rightOrLeftDiff + "++++" + jumpOrSwipeDiff);
                        pythonLeft = pythonRight = pythonUp = false;
                        pythonDown = true;
                        eventCooldown = 1f;
                    }
                }

            startPos = PosVec;




        }
        //}
        //}

        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught", ex);
            Console.Read();
        }

        // Decrement the cooldown timer each frame
        eventCooldown = Mathf.Max(0f, eventCooldown - Time.deltaTime);
    }

   
}
