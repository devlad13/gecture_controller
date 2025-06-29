using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SmoothLoader : MonoBehaviour
{
  private AsyncOperation loadOperation;

  [SerializeField]
  private Slider progressBar;

  private float currentValue;
  private float targetValue;

  [SerializeField]
  [Range(0f, 1f)]
  private float progressAnimationMultiplier = 0.25f;

  private void Start()
  {
    progressBar.value = currentValue = targetValue = 0f;

    var currentScene = SceneManager.GetActiveScene();

    loadOperation = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);

    loadOperation.allowSceneActivation = false;
  }

  private void Update()
  {
    targetValue = loadOperation.progress / 0.9f;

    currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
    progressBar.value = currentValue;

    if (Mathf.Approximately(currentValue, 1))
    {
      loadOperation.allowSceneActivation = true;
    }
    
  }
}
