using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuadWithSpheres : MonoBehaviour
{
  public GameObject[] spheres;

  private MeshRenderer meshRenderer;
  private MeshFilter meshFilter;
  private Mesh mesh;

  public Material material;
  public void Start()
  {
    meshRenderer = gameObject.AddComponent<MeshRenderer>();
    meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
    meshFilter = gameObject.AddComponent<MeshFilter>();
    meshRenderer.material = material;

  }

  private void Update()
  {
    
    mesh = new Mesh();

    Vector3 s1 = spheres[0].transform.position;
    Vector3 s2 = spheres[1].transform.position;
    Vector3 s3 = spheres[2].transform.position;
    Vector3 s4 = spheres[3].transform.position;
    Vector3[] vertices = new Vector3[4]
    {
           s1,s2,s3,s4
    };
    mesh.vertices = vertices;

    int[] tris = new int[12]
    {
            0, 2, 1,
            0, 3, 2,
            0, 1, 3,
            1, 2, 3
    };
    mesh.triangles = tris;

    Vector3[] normals = new Vector3[4]
    {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
    };
    mesh.normals = normals;

    Vector2[] uv = new Vector2[4]
    {
           new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
    };
    mesh.uv = uv;

    meshFilter.mesh = mesh;

  }
}





