using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanCorners : MonoBehaviour
{

    List<Vector3> LocalVertices ;
    List<Vector3> GlobalVertices;
    List<Vector3> Cornervertices;
    List<int> CornerIDs = new List<int>() { 0,10,110,120};
    float radius = 0.5f;
      // Start is called before the first frame update
      void Start()
      {
          GlobalVertices = new List<Vector3> ();
          Cornervertices = new List<Vector3> ();

      GetVertices();
      transform.hasChanged = false;
      }

      // Update is called once per frame
      void Update()
      {
      if (transform.hasChanged)
      {
        GetVertices();
        Debug.Log("Vertices calcullated");
        transform.hasChanged = false;
      }
      }

      void GetVertices()
    {
      LocalVertices = new List<Vector3>(GetComponent<MeshFilter>().mesh.vertices);
      GlobalVertices.Clear ();

      foreach(Vector3 point in LocalVertices)
      {
        GlobalVertices.Add(transform.TransformPoint(point));
      }

      Cornervertices.Clear ();

      foreach(int id in CornerIDs)
      {
        Cornervertices.Add(GlobalVertices[id]);
      }

      foreach(Vector3 point in Cornervertices)
      {
        Debug.Log("point"+ point);
      }
    }

    private void OnDrawGizmos()
    {
      if (GlobalVertices == null)
        return;

      /*Gizmos.color = Color.yellow;

      foreach(Vector3 point in GlobalVertices)
      {
        Gizmos.DrawSphere(point, radius);
      }*/
      Gizmos.color = Color.green;

      foreach (Vector3 point in Cornervertices)
      {
        Gizmos.DrawSphere(point, radius);
      }
    }
}
