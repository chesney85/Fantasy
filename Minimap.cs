using System;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    // Update is called once per frame
    private RectTransform t;
    private void Start()
    {
        t = GetComponent<RectTransform>();
    }

    void Update()
    {
        Quaternion rot = player.rotation;
        rot.z = rot.y;
        rot.x = 0;
        rot.y = 0;
        t.rotation = rot;
    }
}
