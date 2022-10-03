using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Random = UnityEngine.Random;

public class BreathingMinigame : MonoBehaviour
{
    [Header("Debug:")]
    [SerializeField] private int score = 0;
    [SerializeField] public float time = 0f;
    [SerializeField] private float yVelo = 0f;
    [SerializeField] private Vector3 targetY;
    [SerializeField] private Vector3 beginY;

    //private bool isFaded = true;

    [Header("Required Refs:")]
    public MinigameManger MinigameManger;
    public Transform mainZone;
    public Transform topZone;
    public Transform bottomZone;
    public Transform topPoint;
    public Transform bottomPoint;
    public GameObject pointGroup;
    public GameObject redLineIndicator;
    public AudioClip breathMoveSFX;
    public AudioSource audioSource;

    [Header("Configs:")]
    public float mainZoneRange = 3f;
    public float sideZoneRange = 3f;
    public int mainZonePointGainPPS = 3;
    public int sideZonePointGainPPS = 1;
    public bool missedZonesPointLossOn = false;
    public int missedZonesPointLossPPS = -1;
    public float maximumMovementDistance = 100f;
    public float speed = 0.2f;
    public float gravity = 0.2f;
    public float jump = 0.5f;
    public float termialVelo = 5f;
    public float minCooldown = 5f;
    public float maxCooldown = 10f;
    public float length = 120f;

    public void BreathingStart()
    {
        redLineIndicator.transform.localPosition = new Vector3(0, 457, 0);
        StopAllCoroutines();
        StartCoroutine(BreathingEvent());
    }
    public void BreathingEnd()
    {
        redLineIndicator.transform.localPosition = new Vector3(0, 457, 0);
        StopAllCoroutines();
    }
    public IEnumerator Timer()
    {
        time = 0f;
        while (time<length)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator BreathingEvent()
    {
        time = 0f;
        StartCoroutine(Timer());    
        StartCoroutine(BreathLine());
        StartCoroutine(WinCheck());
        while (time < length)
        {
            float cooldown = Random.Range(minCooldown, maxCooldown);
            yield return new WaitForSeconds(cooldown);
            Debug.Log("Finished Cooldown: " + (Mathf.Round(cooldown * 100) / 100) + "s");
            StartCoroutine(Transform());
            yield return Transform();
        }
        Debug.Log("Minigame Ended");
    }
    public IEnumerator BreathLine()
    {
        yVelo = 0f;
        while (time < length)
        {   
            if (Keyboard.current.spaceKey.isPressed && redLineIndicator.transform.localPosition.y < topPoint.localPosition.y && yVelo < termialVelo )
            {
                yVelo += jump * Time.deltaTime;
                yield return null;
            }
            else if (redLineIndicator.transform.localPosition.y > bottomPoint.localPosition.y && yVelo > termialVelo * -1)
            {
                yVelo -= gravity * Time.deltaTime;
                yield return null;
            }
            else if (redLineIndicator.transform.localPosition.y < bottomPoint.localPosition.y)
            {
                yVelo = yVelo * -1 + 5f;
                yield return null;
            }   
            redLineIndicator.transform.localPosition += new Vector3(0, (yVelo*100), 0) * Time.timeScale;
            yield return null;
            if (time >= length)
            {
                MinigameManger.BreathMiniGame();
                MinigameManger.progressionWin();
            }
        }
    }

    public IEnumerator Transform()
    {
        audioSource.PlayOneShot(breathMoveSFX);
        float startTime = Time.time;
        targetY = new Vector3(pointGroup.transform.localPosition.x, Random.Range(topPoint.localPosition.y - maximumMovementDistance, bottomPoint.localPosition.y + maximumMovementDistance), pointGroup.transform.localPosition.z);
        float distanceTotal = Vector3.Distance(pointGroup.transform.localPosition, targetY);
        while (Math.Round(pointGroup.transform.localPosition.y) != Math.Round(targetY.y))
        {
            float distanceCompleted = (Time.time - startTime) * speed;
            float percentage = distanceCompleted / distanceTotal;
            pointGroup.transform.localPosition = Vector3.Lerp(pointGroup.transform.localPosition, targetY, percentage) * Time.timeScale;
            yield return null;
        }
    } 
    public IEnumerator WinCheck()
    {
        yield return new WaitForSeconds(3);
        while (time < length)
        {
            if (redLineIndicator.transform.localPosition.y == mainZone.localPosition.y || (redLineIndicator.transform.localPosition.y < (mainZone.position.y + mainZoneRange) && redLineIndicator.transform.localPosition.y > (mainZone.localPosition.y - mainZoneRange)))
            {
                score += mainZonePointGainPPS;
                time += mainZonePointGainPPS / 5;
                Debug.Log("Hit Main zone, Adding " + mainZonePointGainPPS + " point/s. New point total of: " + score);
            }
            else if (redLineIndicator.transform.localPosition.y == topZone.localPosition.y || (redLineIndicator.transform.localPosition.y < (topZone.localPosition.y + sideZoneRange) && redLineIndicator.transform.localPosition.y > (bottomZone.localPosition.y - sideZoneRange)) || redLineIndicator.transform.localPosition.y == bottomZone.localPosition.y || (redLineIndicator.transform.localPosition.y < (bottomZone.localPosition.y + sideZoneRange) && redLineIndicator.transform.localPosition.y > (bottomZone.localPosition.y - sideZoneRange)))
            {
                score += sideZonePointGainPPS;
                time += sideZonePointGainPPS / 5;
                Debug.Log("Hit Side zone, Adding " + sideZonePointGainPPS + " point/s. New point total of: " + score);
            }
            else if (missedZonesPointLossOn)
            {
                score += missedZonesPointLossPPS;
                time += missedZonesPointLossPPS / (15/2);
                Debug.Log("Missed Zones, Removing " + missedZonesPointLossPPS + " point/s. New point total of: " + score);
            }
            yield return new WaitForSeconds(1);
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 0, 0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(topZone.position + offset, new Vector3(1, sideZoneRange/10, 0));    
        Gizmos.DrawWireCube(bottomZone.position + offset, new Vector3(1, sideZoneRange/10, 0));
        Gizmos.DrawWireCube(mainZone.position + offset, new Vector3(1, mainZoneRange/10, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(redLineIndicator.transform.position + offset, new Vector3(5, 1, 0));
        Gizmos.color = Color.yellow;    
        Gizmos.DrawWireCube(targetY, new Vector3(5, 1, 0));
    }
}
