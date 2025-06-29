using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Mediapipe.Unity;
using Mediapipe.Unity.UI;
using Debug = UnityEngine.Debug;


public class ExeRunner : MonoBehaviour
{
    private Process process1;
    public Camera mainCamera;
    private Solution _solution;
    public bool btnClick;
    public GameObject model;
    public GameObject rig;
    private bool camreset;
    private float progress = 0f;
    private float fillSpeed = 2f;
    public GameObject Quad;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool shouldMove = false;
    public float moveDuration = 1.0f;

    private void Start()
    {
        Debug.Log("path  " + Application.persistentDataPath);

        _solution = GameObject.Find("Solution").GetComponent<Solution>();
        btnClick = true;
        camreset = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.enabled = !mainCamera.enabled;
            rig.SetActive(true);
            camreset = true;
            btnClick = true;
            _solution.Play();
        }

        if (camreset)
        {
            model.GetComponent<Modal>().CloseAndResume(true);
            camreset = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        string name = transform.tag;
        string folder = transform.name;
        string file = Application.persistentDataPath+ "/FS_Build/"+ folder+"/"+ name;

        Debug.Log("path ==" + file);
        progress += (Time.deltaTime * fillSpeed) / 6;
        progress = Mathf.Clamp01(progress);
        Debug.Log("progress " + progress);

        if (transform.tag != "Next" && transform.tag != "Prev")
        {
            if (btnClick && progress >= 1)
            {
                try
                {
                    if (File.Exists(file))
                    {
                        progress = 0;
                        rig.SetActive(false);
                        StartProcess(file);
                    }
                    else
                    {
                        Debug.LogError("File does not exist: " + file);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to start process: " + e.Message);
                }
            }
        }
        else if (transform.tag == "Next" && progress >= 1 && !isMoving)
        {
            targetPosition = new Vector3(Quad.transform.position.x + 122, Quad.transform.position.y, Quad.transform.position.z);
            shouldMove = true;
        }
        else if (transform.tag == "Prev" && progress >= 1 && !isMoving)
        {
            targetPosition = new Vector3(Quad.transform.position.x - 122, Quad.transform.position.y, Quad.transform.position.z);
            shouldMove = true;
        }

        if (shouldMove)
        {
            shouldMove = false;
            StartCoroutine(MoveOverTime(targetPosition, moveDuration));
        }
    }
    private IEnumerator MoveOverTime(Vector3 target, float duration)
    {
        isMoving = true;
        Vector3 start = Quad.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Quad.transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Quad.transform.position = target; // Ensure it snaps to the target position
        isMoving = false;
        progress = 0; // Reset progress for next interaction
    }
    private void StartProcess(string file)
    {
        if (process1 != null)
        {
            Debug.LogWarning("Process is already running. Disposing the old process first.");
            process1.Dispose();
            process1 = null;
        }

        process1 = new Process();
        process1.StartInfo.FileName = file;
        process1.EnableRaisingEvents = true;
        process1.Exited += OnProcessExited;

        try
        {
            Debug.Log("Starting process: " + file);
            process1.Start();
            Debug.Log("Process started");
            btnClick = false;
            camreset = false;
            _solution.Stop();
        }
        catch (Exception e)
        {
            Debug.LogError("Error starting process: " + e.Message);
            process1.Dispose();
            process1 = null;
        }
    }

    private void OnProcessExited(object sender, EventArgs e)
    {
        try
        {
            Debug.Log("Process exited");

            progress = 0;
            btnClick = true;
            camreset = true;

            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                rig.SetActive(true);
                _solution.Play();
                mainCamera.enabled = !mainCamera.enabled;
            });
        }
        catch (Exception ex)
        {
            Debug.LogError("Error during process exit handling: " + ex.Message);
        }
        finally
        {
            if (process1 != null)
            {
                process1.Dispose();
                process1 = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        progress = 0;
    }

    private void OnDestroy()
    {
        if (process1 != null)
        {
            process1.Dispose();
            process1 = null;
        }
    }
}
