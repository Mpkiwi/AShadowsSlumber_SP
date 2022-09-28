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
    public CamControl playerCam;
    public LightToggle darkLights;
    
    public bool gameActive = false;

    public void gameTrigger()
    {
        Debug.Log("Recived Game Trigger");
        if(SceneManager.GetActiveScene().buildIndex == 3 && !gameActive)
        {
            darkLights.lightFlickStart();
            Debug.Log("Starting Lights");
        }
        if (gameActive)
        {
            darkLights.lightFlickStop();
        }
    }
    public void HeartMiniGame()
    {
        if (!gameActive)
        {
            HeartBeatInt();
            gameActive = true;
            playerCam.camDisabled = true;
        }
        else
        {
            HeartBeatEnd();
            gameActive = false;
            playerCam.camDisabled = false;
        }
    }
    public void BreathMiniGame()
    {
        if (!gameActive)
        {
            BreathInt();
            gameActive = true;
            playerCam.camDisabled = true;
        }
        else
        {
            BreathEnd();
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
        HeartbeatGUI.SetActive(false);
    }
    public void BreathInt()
    {
        BreathGUI.SetActive(true);
        BreathingMinigame.BreathingStart();
    }
    public void BreathEnd()
    {
        BreathGUI.SetActive(false); 
    }
}

