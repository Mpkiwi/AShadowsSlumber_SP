using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public AudioMixer musicmixer;

    public TMPro.TMP_Dropdown rezdropdown;

    public CamControl playerCam;

    public static float mouseSensitivity = 420f;
    public static float camFov = 70f;
    public static float InvertXValue = 1f;
    public static float InvertYValue = 1f;
    
    Resolution[] resolutions;
    private void Start()
    {
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
        QualitySettings.SetQualityLevel(quality);
    }
    public void Fullscreen(bool setFull)
    {
        Debug.Log("Player.FullScreen" + setFull);
        Screen.fullScreen = setFull;
    }
    public void Vsync(bool vSyncOn)
    {   
        if (vSyncOn == true)
        {
            QualitySettings.vSyncCount = 1;
        }
        else if (vSyncOn == false)
        {
            QualitySettings.vSyncCount = 0;
        }
    }
    public void AudioVolume(float volume)
    {
        mixer.SetFloat("mainVolume", Mathf.Log10(volume) * 20);
    }
    public void MusicVolume(float mvolume)
    {
        musicmixer.SetFloat("musicVolume", Mathf.Log10(mvolume) * 20);
    }

    public void MouseSense(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }
    public void CameraFov(float FOV)
    {
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
            InvertXValue = -1f;
        }
        else if (xInvert == false)
        {
            InvertXValue = 1f;
        }
    }
    public void InvertY(bool yInvert)
    {
        if (yInvert == true)
        {
            InvertYValue = -1f;
        }
        else if (yInvert == false)
        {
            InvertYValue = 1f;
        }
    }
}   
