using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Event : MonoBehaviour
{

    //float progress = 0f;
    //float fillSpeed = 2f;
    public GameObject[] gameobj;
    //public string Path;
    private void Start()
    {
        StartCoroutine(Timer());
    }

   
    IEnumerator Timer()
    {
        

        yield return new WaitForSeconds(1);
        for (int i = 0; i < gameobj.Length; i++)
        {
            gameobj[i].SetActive(true);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
       

        if (other.tag == "Next" || other.tag == "Prev")
        {
            other.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);

        }
        else
        {
            other.transform.localScale = new Vector3(15, 10, 10);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Next" || other.tag=="Prev")
        {
            other.transform.localScale = new Vector3(1.85f, 1.85f, 1.85f);

        }
        else
        {
            other.transform.localScale = new Vector3(13, 8, 8);
        }
    }
}
