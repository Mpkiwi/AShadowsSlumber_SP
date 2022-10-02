using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor.Rendering;
using System;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public AudioMixer musicmixer;

    public TMPro.TMP_Dropdown rezdropdown;

    public CamControl playerCam;

    public Brightness brightness;

    public static float mouseSensitivity = 420f;
    public static float camFov = 70f;
    public static float InvertXValue = 1f;
    public static float InvertYValue = 1f;


    Resolution[] resolutions;
    private void Start()
    {
        Quality(PlayerPrefs.GetInt("qualitySet"));
        Fullscreen(Convert.ToBoolean(PlayerPrefs.GetInt("isFullscreen")));
        Vsync(Convert.ToBoolean(PlayerPrefs.GetInt("isVsync")));
        CameraFov(PlayerPrefs.GetFloat("FOV"));
        AudioVolume(PlayerPrefs.GetFloat("audioVolume"));
        MusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        MouseSense(PlayerPrefs.GetFloat("mouseSense"));
        InvertX(Convert.ToBoolean(PlayerPrefs.GetInt("invertX")));
        InvertY(Convert.ToBoolean(PlayerPrefs.GetInt("invertY")));
        

        resolutions = Screen.resolutions;

        rezdropdown.ClearOptions();

        List<string> rezoptions = new List<string>();

        int currentRez = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            rezoptions.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            { 
                currentRez = i;
            }
        }

        rezdropdown.AddOptions(rezoptions);
        rezdropdown.value = currentRez;
        rezdropdown.RefreshShownValue(); 
    }

    public void SetResolution(int Indresolution)
    {
        Resolution resolution = resolutions[Indresolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void Quality(int quality)
    {
        PlayerPrefs.SetInt("qualitySet", quality);
        QualitySettings.SetQualityLevel(quality);
    }
    public void Fullscreen(bool setFull)
    {
        if (setFull)
        {
            PlayerPrefs.SetInt("isFullscreen", 1);
        }
        else if (!setFull)
        {
            PlayerPrefs.SetInt("isFullscreen", 0);
        }
        Debug.Log("Player.FullScreen" + setFull);
        Screen.fullScreen = setFull;
    }
    public void Vsync(bool vSyncOn)
    {   
        if (vSyncOn)
        {
            PlayerPrefs.SetInt("isVsync", 1);
            QualitySettings.vSyncCount = 1;
        }
        else if (!vSyncOn)
        {
            PlayerPrefs.SetInt("isVsync", 0);
            QualitySettings.vSyncCount = 0;
        }
    }
    public void AudioVolume(float volume)
    {
        PlayerPrefs.SetFloat("audioVolume", volume);
        mixer.SetFloat("mainVolume", Mathf.Log10(volume) * 20);
    }
    public void MusicVolume(float mvolume)
    {
        PlayerPrefs.SetFloat("musicVolume", mvolume);
        musicmixer.SetFloat("musicVolume", Mathf.Log10(mvolume) * 20);
    }

    public void MouseSense(float sensitivity)
    {
        PlayerPrefs.SetFloat("mouseSense", sensitivity);
        mouseSensitivity = sensitivity;
    }
    public void CameraFov(float FOV)
    {
        PlayerPrefs.SetFloat("FOV", FOV);
        camFov = FOV;
        if (playerCam)
        {
            playerCam.FovChange();
        }
    }
    public void InvertX(bool xInvert)
    { 
        if (xInvert == true)
        {
            PlayerPrefs.SetInt("invertX", 1);
            InvertXValue = -1f;
        }
        else if (xInvert == false)
        {
            PlayerPrefs.SetInt("invertX", 0);
            InvertXValue = 1f;
        }
    }
    public void InvertY(bool yInvert)
    {
        if (yInvert == true)
        {
            PlayerPrefs.SetInt("invertY", 1);
            InvertYValue = -1f;
        }
        else if (yInvert == false)
        {
            PlayerPrefs.SetInt("invertY", 0);
            InvertYValue = 1f;
        }
    }
}   
