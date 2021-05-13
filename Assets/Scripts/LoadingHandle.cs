using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandle : MonoBehaviour
{
    [SerializeField]
    GameObject loadCanvas;
    void Start()
    {
        loadCanvas.SetActive(true);
        StartCoroutine(WaitForLoadingEnum());
    }

    IEnumerator WaitForLoadingEnum()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        loadCanvas.SetActive(false);
    }
}
