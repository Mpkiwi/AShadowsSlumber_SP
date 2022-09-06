using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SecondCamUI : MonoBehaviour
{
    public Camera cam;

    [SerializeField] private float fov;
    void Start()
    {
        fov = SettingsMenu.camFov;
        cam.fieldOfView = fov;
    }
}
