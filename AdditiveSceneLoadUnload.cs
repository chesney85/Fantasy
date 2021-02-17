using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoadUnload : MonoBehaviour
{

    public string sceneToLoad;

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad,LoadSceneMode.Additive);
    }

    void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(sceneToLoad, UnloadSceneOptions.None);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnloadScene();
        }
    }
}
