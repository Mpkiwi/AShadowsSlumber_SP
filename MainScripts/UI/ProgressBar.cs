using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ProgressBar : MonoBehaviour
{
    public Image progressBar;
    public HeartBeatMinigame HeartBeatMinigame;
    public BreathingMinigame BreathingMinigame;
    private float time;
    public void barHBStart()
    {
        barEnd();
        StartCoroutine(hearbeatUpdater());
    }
    public void barBStart()
    {
        barEnd();
        StartCoroutine(breathUpdater());
    }
    public void barEnd()
    {
        progressBar.fillAmount = 0;
        StopAllCoroutines();
    }
    public IEnumerator hearbeatUpdater()
    {
        Debug.Log("Bar HeartBeat Updater");
        while (HeartBeatMinigame.time < HeartBeatMinigame.length)
        {
            progressBar.fillAmount = HeartBeatMinigame.time / HeartBeatMinigame.length;
            yield return null;
        }
    }
    public IEnumerator breathUpdater()
    {
        Debug.Log("Bar Breath Updater");
        while (BreathingMinigame.time < BreathingMinigame.length)
        {            
            progressBar.fillAmount = BreathingMinigame.time / BreathingMinigame.length;
            yield return null;
        }
    }
}
