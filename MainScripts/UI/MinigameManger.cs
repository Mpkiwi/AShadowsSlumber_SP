using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManger : MonoBehaviour
{
    public GameObject HeartbeatGUI;
    public HeartBeatMinigame HeartBeatMinigame;
    public GameObject BreathGUI;
    public BreathingMinigame BreathingMinigame;
    public CamControl playerCam;

    public bool gameActive = false;

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
    private void HeartBeatInt()
    {
        HeartbeatGUI.SetActive(true);
        HeartBeatMinigame.HeartBeatStart();
    }
    private void HeartBeatEnd()
    {
        HeartbeatGUI.SetActive(false);
    }
    private void BreathInt()
    {
        BreathGUI.SetActive(true);
        BreathingMinigame.BreathingStart();
    }
    private void BreathEnd()
    {
        BreathGUI.SetActive(false); 
    }
}

