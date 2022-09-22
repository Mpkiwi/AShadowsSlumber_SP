using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManger : MonoBehaviour
{
    public GameObject HeartbeatGUI;
    public HeartBeatMinigame HeartBeatMinigame;
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
    private void HeartBeatInt()
    {
        HeartbeatGUI.SetActive(true);
        HeartBeatMinigame.HeartBeatStart();
    }
    private void HeartBeatEnd()
    {
        HeartbeatGUI.SetActive(false);
    }

