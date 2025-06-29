using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution1 : MonoBehaviour
{
    public GameObject verticle;
    public GameObject horizontal;
    // Start is called before the first frame update
    void Start()
    {
        if ( Screen.width > 1080)
        {
            horizontal.SetActive(true);
            verticle.SetActive(false);
        }
        else if ( Screen.width <= 1080)
        {
            horizontal.SetActive(false);
            verticle.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width >1080)
        {
            horizontal.SetActive(true);
            verticle.SetActive(false);
        }
        else if (Screen.width <= 1080)
        {
            horizontal.SetActive(false);
            verticle.SetActive(true);
        }
    }
}
