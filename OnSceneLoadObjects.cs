using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoadObjects : MonoBehaviour
{
    public List<GameObject> objectsToLoad;
    private int index;
    private void OnEnable()
    {
        index = 0;
        StartCoroutine(nameof(LoadObjects));
    }

    IEnumerator LoadObjects()
    {
        if (index < objectsToLoad.Count-1)
        {
            objectsToLoad[index].SetActive(true);
            index++;
            yield return null;
        }
    }

    IEnumerator UnLoadObjects()
    {
        if (index < objectsToLoad.Count-1)
        {
            objectsToLoad[index].SetActive(true);
            index++;
            yield return null;
        }
    }

    private void OnDisable()
    {
        StartCoroutine(nameof(UnLoadObjects));
    }
}
