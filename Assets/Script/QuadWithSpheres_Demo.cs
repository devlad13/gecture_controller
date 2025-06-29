using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuadWithSpheres_Demo : MonoBehaviour
{
  public GameObject[] spheres;
  public GameObject quad;

  public float z = 89.12f;
  public void Update()
  {
    
    Vector3 s1 = spheres[0].transform.localPosition;
    Vector3 s2 = spheres[1].transform.localPosition;
    Vector3 s3 = spheres[2].transform.localPosition;
    Vector3 s4 = spheres[3].transform.localPosition;

    Vector3 centerPoint = (s1 + s2 + s3 + s4) / 4;

    float distance = Vector3.Distance(s1, s3);
    float distance1 = Vector3.Distance(s1, s2);

    quad.transform.localPosition = new Vector3(centerPoint.x, centerPoint.y, z); ;

    quad.transform.localScale = new Vector3(distance1+2, distance+2, distance1);

    Vector3 Direction = (s1 - s2).normalized;
    quad.transform.right = Direction;

  }
}





