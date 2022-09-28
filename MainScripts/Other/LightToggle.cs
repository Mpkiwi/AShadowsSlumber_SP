using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    [Header("Debug:")]
    public bool isOn = true;
    [Header("Required Refs:")]
    public Light spotlight;
    [Header("Configs:")]
    public float minCooldown = 10f;
    public float maxCooldown = 20f;
    public int minlightloops = 1;
    public int maxlightloops = 7;
    public bool longDark = true;
    public float minLongDark = 10f;
    public float maxLongDark = 20f;


    public void lightFlickStart()
    {
        isOn = true;
        StartCoroutine(lightLoop()); 
    }
    public IEnumerator lightLoop()
    {
        while (isOn)
        {
            float cooldown = Random.Range(minCooldown, maxCooldown);
            yield return new WaitForSeconds(cooldown);
            Debug.Log("Finished Cooldown: " + (Mathf.Round(cooldown * 100) / 100) + "s");
            for (int i = 0; i < Random.Range(minlightloops, maxlightloops); i++)
            {
                spotlight.enabled = false;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                spotlight.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
            }
            spotlight.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.3f, 1f));
            spotlight.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            spotlight.enabled = false;
            yield return new WaitForSeconds(Random.Range(10f, 20f));
            spotlight.enabled = true;
        }
    }
    public void lightFlickStop()
    {
        spotlight.enabled = true;
        isOn = false;
        StopAllCoroutines();
    }
}
