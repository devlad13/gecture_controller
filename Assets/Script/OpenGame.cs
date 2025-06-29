using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

//public class OpenGame : MonoBehaviour
//{
//    float progress = 0f;
//    float fillSpeed = 2f;
//    public string Path;
//    private Process mProcess;

//    private void OnTriggerEnter(Collider other)
//    {
//        string name = transform.tag;
//        string FullPath = Path + name;
       


//        progress += Time.deltaTime * fillSpeed;
//        progress = Mathf.Clamp01(progress);
//        Debug.Log("progress " + progress);
//        //  StartCoroutine(timer(FullPath));
//        if (progress >= 0.25)
//        {
//            Process process = new Process();
//            process.StartInfo.FileName = FullPath;
//            process.Start();

//        }
        
//    }
//    private void OnTriggerExit(Collider other)
//    {
//        string name = transform.tag;
//        string FullPath = Path + name;
//        progress += Time.deltaTime * fillSpeed;
//        progress = Mathf.Clamp01(progress);
//        Debug.Log("progress " + progress);
//        //  StartCoroutine(timer(FullPath));
//        if (progress >= 0.25)
//        {
//            Process process = new Process();
//            process.StartInfo.FileName = FullPath;
//            process.Start();
//            progress = 0;
//        }
//    }
//}
