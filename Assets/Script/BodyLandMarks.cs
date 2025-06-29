using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

namespace Mediapipe.Unity.PoseTracking
{
    public class BodyLandMarks : MonoBehaviour
  {
    public GameObject pointParent;
    /*public List<Transform> spheres = new List<Transform>();
    public List<int> landMarkNom = new List<int>();
    //public List<GameObject> Joint = new List<GameObject>();
    public List<GameObject> capsules = new List<GameObject>();
    public List<GameObject> Quad = new List<GameObject>();

    public Material shirtMaterial;
    public Material pentMaterial;*/
    public MultiOption activeOptions;
    public List<GameObject> quadList = new List<GameObject>();
    public GameObject CenterObj;

    public static float right_wristx, right_wristy;
    public Vector3 centerPoint;
    public static float PlayerXvalue,PlayerYvalue;
    int PoseDetection;

        public void Start()
        {
            PoseDetection = LayerMask.NameToLayer("PoseDetection");
            /*for (int i = 0; i < 32; i++)
            {
                GameObject Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                Quad.name = "LandMark" + i;
                Quad.SetActive(false);
                Quad.transform.SetParent(this.gameObject.transform);
                Quad.transform.localPosition = Vector3.zero;
                quadList.Add(Quad);
            }*/
        }

      
        public void Update()
        {
            if (pointParent.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                Vector3 right_shoulder = pointParent.transform.GetChild(12).transform.localPosition;
                Vector3 right_hip = pointParent.transform.GetChild(24).transform.localPosition;
                Vector3 left_shoulder = pointParent.transform.GetChild(11).transform.localPosition;
                Vector3 left_hip = pointParent.transform.GetChild(23).transform.localPosition;
                right_wristx = (pointParent.transform.GetChild(16).transform.localPosition.x) /50 ;
                right_wristy = (pointParent.transform.GetChild(16).transform.localPosition.y) /50 ;

                
                centerPoint = (right_shoulder + right_hip + left_shoulder + left_hip) / 4;
                PlayerXvalue = centerPoint.x;
                PlayerYvalue = centerPoint.y;
                CenterObj.transform.localPosition = centerPoint;
            }
            else
            {
                PlayerXvalue = 0;
            }

            for (int i = 0; i < 32; i++)
            {
                MultiOption option = (MultiOption)(1 << i);

                // Check if the current option is active
                if ((activeOptions & option) == option)
                {
                    // Activate the corresponding sphere
                    //GameObject Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    //quadList[i].SetActive(true);
                    //quadList[i].transform.localPosition = pointParent.transform.GetChild(i).transform.position;
                    pointParent.transform.GetChild(i).gameObject.layer = default;
                }
                else
                {
                    // Deactivate the corresponding sphere
                    //quadList[i].SetActive(false);
                    pointParent.transform.GetChild(i).gameObject.layer = PoseDetection;
                }
            }
            //Debug.Log("<color=green> Center Position:: </color>" + (float)right_wristx+"++++"+(float)right_wristy);

            /*int arrayIndex = 0;
          for (int i = 0; i < 33; i++)
          {
            if (landMarkNom.Contains(i))
            {
              spheres[arrayIndex].localPosition = new Vector3(pointParent.transform.GetChild(landMarkNom[arrayIndex]).transform.position.x, pointParent.transform.GetChild(landMarkNom[arrayIndex]).transform.position.y, pointParent.transform.GetChild(landMarkNom[arrayIndex]).transform.position.z);
          
              //rigObject[arrayIndex].transform.localPosition = spheres[arrayIndex].localPosition;
              if (pointParent.transform.GetChild(landMarkNom[arrayIndex]).gameObject.activeInHierarchy)
              {

                //spheres[arrayIndex].gameObject.SetActive(true);
                //capculeSpawner(11, 12, capsules[0]);
                //capculeSpawner(12, 24, capsules[1]);
                //capculeSpawner(24, 23, capsules[2]);
                //capculeSpawner(23, 11, capsules[3]);
                //capculeSpawner(15, 16, capsules[4]);

                //quadForTwopoints(12, 14, Quad[0]);
                //quadForTwopoints(14, 16, Quad[1]);
                //quadForTwopoints(11, 13, Quad[2]);
                //quadForTwopoints(13, 15, Quad[3]);
                //quadForTwopoints(24, 26, Quad[4]);
                //quadForTwopoints(26, 28, Quad[5]);
                //quadForTwopoints(23, 25, Quad[6]);
                //quadForTwopoints(25, 27, Quad[7]);

                //if (Quad[8].gameObject.activeInHierarchy)
                //{
                //  quadForFourpoints(12, 24, 11, 23, Quad[8]);
                //}
              }
              else
              {
                spheres[arrayIndex].gameObject.SetActive(false); 
              }
              arrayIndex++;
            }
          }*/


        }
      
        ///////
        #region old
        /*  public void capculeSpawner(int start, int end, GameObject capsule)
      {
        Vector3 capsulePosition = (spheres[landMarkNom.IndexOf(start)].transform.position + spheres[landMarkNom.IndexOf(end)].transform.position) / 2f;
        capsule.transform.position = capsulePosition;

        float distance = Vector3.Distance(spheres[landMarkNom.IndexOf(start)].localPosition, spheres[landMarkNom.IndexOf(end)].localPosition);
        capsule.transform.localScale = new Vector3(1f, 1f, distance);

        Vector3 direction = spheres[landMarkNom.IndexOf(end)].transform.position - spheres[landMarkNom.IndexOf(start)].transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        capsule.transform.rotation = rotation;

        // Access the capsule collider component
        CapsuleCollider capsuleCollider = capsule.GetComponent<CapsuleCollider>();

        // Calculate the ratio between length and radius
        float radiusRatio = 0.1f; // Adjust this value as needed
        float newRadius = distance * radiusRatio;

        // Set the collider height to match the capsule's length
        capsuleCollider.height = 1;

        // Set the collider radius based on the calculated ratio
        capsuleCollider.radius = newRadius;

        *//*capsule.GetComponent<CapsuleCollider>().height = 1;
        capsule.GetComponent<CapsuleCollider>().radius = 10;*//*
        capsule.GetComponent<CapsuleCollider>().direction = 2;
      }

      public void quadForTwopoints(int start, int end, GameObject quad)
      {
        Vector3 sphere1Position = spheres[landMarkNom.IndexOf(start)].transform.localPosition;
        Vector3 sphere2Position = spheres[landMarkNom.IndexOf(end)].transform.localPosition;
        Vector3 centerPoint = (sphere1Position + sphere2Position) / 2;

        // Calculate the distance between the spheres
        float distance = Vector3.Distance(sphere1Position, sphere2Position);

        // Position the plane at the center point
        quad.transform.localPosition = new Vector3(centerPoint.x, centerPoint.y, z);

        // Set the scale of the plane
        quad.transform.localScale = new Vector3(distance, distance / 2, distance);

        Vector3 forwardDirection = (sphere2Position - sphere1Position).normalized;
        quad.transform.right = forwardDirection;
      }

      public void quadForFourpoints(int rightUp,int rightDown,int leftUp,int leftDown, GameObject quad)
      {
        Vector3 s1 = spheres[landMarkNom.IndexOf(rightUp)].transform.localPosition;
        Vector3 s2 = spheres[landMarkNom.IndexOf(rightDown)].transform.localPosition;
        Vector3 s3 = spheres[landMarkNom.IndexOf(leftUp)].transform.localPosition;
        Vector3 s4 = spheres[landMarkNom.IndexOf(leftDown)].transform.localPosition;

        Vector3 centerPoint = (s1 + s2 + s3 + s4) / 4;

        float distance = Vector3.Distance(s1, s3);
        float distance1 = Vector3.Distance(s1, s2);

        quad.transform.localPosition = new Vector3(centerPoint.x, centerPoint.y, z); ;

        quad.transform.localScale = new Vector3(distance1 + 2, distance + 2, distance1);

        Vector3 Direction = (s1 - s2).normalized;
        quad.transform.right = Direction;
      }*/
        #endregion
    }

    [System.Flags]
    public enum MultiOption
    {
        None = 0,
        Nose = 1 << 0,
        LeftEyeInner = 1 << 1,
        LeftEye = 1 << 2,
        LeftEyeOuter = 1 << 3,
        RightEyeInner = 1 << 4,
        RightEye = 1 << 5,
        RightEyeOuter = 1 << 6,
        LeftEar = 1 << 7,
        RightEar = 1 << 8,
        MouthLeft = 1 << 9,
        MouthRight = 1 << 10,
        LeftShoulder = 1 << 11,
        RightShoulder = 1 << 12,
        LeftElbow = 1 << 13,
        RightElbow = 1 << 14,
        LeftWrist = 1 << 15,
        RightWrist = 1 << 16,
        LeftPinky = 1 << 17,
        RightPinky = 1 << 18,
        LeftIndex = 1 << 19,
        RightIndex = 1 << 20,
        LeftThumb = 1 << 21,
        RightThumb = 1 << 22,
        LeftHip = 1 << 23,
        RightHip = 1 << 24,
        LeftKnee = 1 << 25,
        RightKnee = 1 << 26,
        LeftAnkle = 1 << 27,
        RightAnkle = 1 << 28,
        LeftHeel = 1 << 29,
        RightHeel = 1 << 30,
        LeftFootIndex = 1 << 31,
        RightFootIndex = 1 << 32,
        
        
    }

    
    
}
