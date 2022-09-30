using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class MinigameManger : MonoBehaviour
{
    public GameObject HeartbeatGUI;
    public HeartBeatMinigame HeartBeatMinigame;
    public GameObject BreathGUI;
    public BreathingMinigame BreathingMinigame;
    public ProgressBar ProgressBar;
    public CamControl playerCam;
    public LightToggle darkLights;
    public GameObject darknessParticles;
    public GameObject progressBarGroup;
 
    
    public bool gameActive = false;

    public void gameTrigger()
    {
        Debug.Log("Recived Game Trigger");
        loadProgressBar();
        if (SceneManager.GetActiveScene().buildIndex == 3 && !gameActive)
        {
            darkLights.lightFlickStart();
            darknessParticles.SetActive(true);
            Debug.Log("Starting Lights & Particles");
        }
        if (gameActive)
        {
            darkLights.lightFlickStop();
            darknessParticles.SetActive(false);
        }
    }

    public void loadProgressBar()
    {
        if(!gameActive)
        {
            progressBarGroup.SetActive(true);
            
        }
        else if (gameActive)
        {
            ProgressBar.barEnd();
            progressBarGroup.SetActive(false); 
        }
    }
    public void HeartMiniGame()
    {
        if (!gameActive)
        {
            HeartBeatInt();
            ProgressBar.barHBStart();
            gameActive = true;
            playerCam.camDisabled = true;
        }
        else
        {
            HeartBeatEnd();
            ProgressBar.barEnd();
            gameActive = false;
            playerCam.camDisabled = false;
        }
    }
    public void BreathMiniGame()
    {
        if (!gameActive)
        {
            BreathInt();
            ProgressBar.barBStart();
            gameActive = true;
            playerCam.camDisabled = true;
        }
        else
        {
            BreathEnd();
            ProgressBar.barEnd();
            gameActive = false;
            playerCam.camDisabled = false;
        }
    }
    public void HeartBeatInt()
    {
        HeartbeatGUI.SetActive(true);
        HeartBeatMinigame.HeartBeatStart();
    }
    public void HeartBeatEnd()
    {
        HeartBeatMinigame.HeartBeatEnd();
        gameTrigger();
        HeartbeatGUI.SetActive(false);
    }
    public void BreathInt()
    {
        BreathGUI.SetActive(true);
        BreathingMinigame.BreathingStart();
    }
    public void BreathEnd()
    {
        BreathingMinigame.BreathingEnd();
        gameTrigger();
        BreathGUI.SetActive(false);
    }
}

