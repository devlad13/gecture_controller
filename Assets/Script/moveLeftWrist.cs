using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeftWrist : MonoBehaviour
{
    public GameObject pointParent;
    public GameObject child;
    public int val_mul, val_plus,min,max;// left shoulder = -30,-50,-350,-180   ////  right shoulder = 30,50,180,400
    public int x; //left 18 right 19
   
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 left = pointParent.transform.GetChild(x).transform.position;

        float z = (left.y * val_mul)+ val_plus;
        z = Clamp(z, min, max);
        child = pointParent.transform.GetChild(x).gameObject;
        Debug.Log("z==== " + z);
        Quaternion zRotation = Quaternion.Euler(0, 0, z);
        transform.rotation = zRotation;
    }
    float Clamp(float value, float min, float max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        return value;
    }
}
