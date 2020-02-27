using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foggy : MonoBehaviour
{
    public float fogDensity = 0.6f;
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = fogDensity;

    }

    private void OnDisable()
    {
        RenderSettings.fog = false;
    }
}
