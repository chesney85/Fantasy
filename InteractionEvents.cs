using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionEvents : MonoBehaviour
{
    public UnityEvent OnBeginFocus;
    public UnityEvent OnEndFocus;


    public void BeginFocus()
    {
        OnBeginFocus.Invoke();
    }

    public void EndFocus()
    {
        OnEndFocus.Invoke();
    }
}
