using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor.Rendering;

public class Brightness : MonoBehaviour
{
    public Slider slider;

    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    static AutoExposure exposure;
    void Start()
    {
        brightness.TryGetSettings(out exposure);
    }
    public void ChangeBrightness(float brightnessValue)
    {
        PlayerPrefs.SetFloat("videoGamma", brightnessValue);
        Debug.Log("Player.Brightness = " + brightnessValue / 10);
        exposure.keyValue.value = brightnessValue / 10;
    }
}
