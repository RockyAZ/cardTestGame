using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandle : MonoBehaviour
{
    [SerializeField]
    GameObject loadCanvas;
    // Start is called before the first frame update
    void Start()
    {
        loadCanvas.SetActive(true);
        StartCoroutine(WaitForLoadingEnum());
    }

    // Update is called once per frame
    IEnumerator WaitForLoadingEnum()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        loadCanvas.SetActive(false);
    }
}
