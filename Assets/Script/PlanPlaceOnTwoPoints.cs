using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanPlaceOnTwoPoints : MonoBehaviour
{
  public List<GameObject> Quads = new List<GameObject>();
  public GameObject sphere1;
  public GameObject sphere2;
  public GameObject plane;
  
  private float z = 89.12f;

  private void Update()
  {
    // Calculate the center point between the two spheres
    Vector3 sphere1Position = sphere1.transform.localPosition;
    Vector3 sphere2Position = sphere2.transform.localPosition;
    Vector3 centerPoint = (sphere1Position + sphere2Position) / 2;

    // Calculate the distance between the spheres
    float distance = Vector3.Distance(sphere1Position, sphere2Position);

    // Position the plane at the center point
    plane.transform.localPosition = new Vector3(centerPoint.x,centerPoint.y, z);

    // Set the scale of the plane
    plane.transform.localScale = new Vector3(distance, distance / 2,distance);

    Vector3 forwardDirection = (sphere2Position - sphere1Position).normalized;
    plane.transform.right = forwardDirection;
  }
}
