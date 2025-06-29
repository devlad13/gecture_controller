using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseOnclick : MonoBehaviour
{
    public GameObject target;
    public Vector3 vector3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    if (this.gameObject == raycastHit.transform.gameObject)
                    {
                        target = raycastHit.transform.gameObject;
                        vector3 = raycastHit.transform.localPosition;
                    }
                    //Our custom method. 

                }
            }
        }
        if (target != null)
        {
            if (this.gameObject == target)
            {

                vector3 = target.transform.localPosition;
            }
        }
    }
}
