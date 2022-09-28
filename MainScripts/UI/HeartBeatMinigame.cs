using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Image = UnityEngine.UI.Image;
using System;
using Random = UnityEngine.Random;

public class HeartBeatMinigame : MonoBehaviour
{
    [Header("Debug:")]
    [SerializeField] public int score = 0;
    [SerializeField] private float time = 0f;
    private bool spaceDown = false;
    //private bool isFaded = true;

    [Header("Required Refs:")]
    public MinigameManger MinigameManger;
    public Transform mainZone;
    public Transform leftZone;
    public Transform rightZone;
    public Transform resetPoint;
    public Transform endpoint;
    public GameObject redLineIndicator;
    public Image heartBeat;
    private Image redlineimg; 

    [Header("Configs:")]
    public float mainZoneRange = 25f;
    public float sideZoneRange = 25f;
    public int mainZonePointGain = 3;
    public int sideZonePointGain = 3;
    public int missedZonesPointLoss = -1;
    public float speed = 0.2f;
    public float minCooldown = 5f;
    public float maxCooldown = 10f;
    public float length = 120f;

   
    public void HeartBeatStart()
    {
        StopAllCoroutines();
        StartCoroutine(HeartBeatEvent());
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
public IEnumerator HeartBeatEvent()
    {
        Image redlineimg = redLineIndicator.GetComponent(typeof(Image)) as Image;
        StartCoroutine(Timer());
        while (time < length)
        {
            float cooldown = Random.Range(minCooldown, maxCooldown);
            yield return new WaitForSeconds(cooldown);
            Debug.Log("Finished Cooldown: " + (Mathf.Round(cooldown * 100) / 100) + "s");
            spaceDown = false;
            redLineIndicator.transform.position = resetPoint.position;
            Color c2 = redlineimg.color;
            float alpha2 = 1f;
            c2.a = alpha2;
            redlineimg.color = c2;
            Color c = heartBeat.color;
            float alpha = 1f;
            c.a = alpha;
            heartBeat.color = c;
            StartCoroutine(Transform());
            yield return Transform();
            WinCheck();
            alpha2 = 0f;
            c2.a = alpha2;
            redlineimg.color = c2;
            StartCoroutine(Fade());
            yield return Fade();
            heartBeat.fillAmount = 0;
            if (time >= length)
            {
                StopAllCoroutines();
                MinigameManger.HeartBeatEnd();
            }
        }
    }

    public IEnumerator Transform()
    {
        while (redLineIndicator.transform.localPosition.x < endpoint.localPosition.x && !spaceDown)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                spaceDown = true;
            }
            heartBeat.fillAmount += 0.01f * speed * Time.deltaTime;
            redLineIndicator.transform.position = Vector3.MoveTowards(redLineIndicator.transform.position, endpoint.position, speed * Time.deltaTime);
            yield return null;
        }
    } 
    IEnumerator Fade()
    {
        Color c = heartBeat.color;  
        for (float alpha = 1f; alpha >= 0; alpha -= 0.01f)
        {
            c.a = alpha;
            heartBeat.color = c;
            yield return null;
            
        }
    }
    void WinCheck()
    {
        if (redLineIndicator.transform.position.x == mainZone.position.x || (redLineIndicator.transform.position.x < (mainZone.position.x + mainZoneRange) && redLineIndicator.transform.position.x > (mainZone.position.x - mainZoneRange)))
        {
            score += mainZonePointGain;
            Debug.Log("Hit Main zone, Adding " + mainZonePointGain + " point/s. New point total of: " + score);
        }
        else if (redLineIndicator.transform.position.x == leftZone.position.x || (redLineIndicator.transform.position.x < (leftZone.position.x + sideZoneRange) && redLineIndicator.transform.position.x > (leftZone.position.x - sideZoneRange)) || redLineIndicator.transform.position.x == rightZone.position.x || (redLineIndicator.transform.position.x < (rightZone.position.x + sideZoneRange) && redLineIndicator.transform.position.x > (rightZone.position.x - sideZoneRange)))
        {
            score += sideZonePointGain;
            Debug.Log("Hit Side zone, Adding " + sideZonePointGain + " point/s. New point total of: " + score);
        }
        else
        {
            score += missedZonesPointLoss;
            Debug.Log("Missed Zones, Removing " + missedZonesPointLoss + " point/s. New point total of: " + score);
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 0, 0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(leftZone.position + offset, new Vector3(sideZoneRange,1,0));
        Gizmos.DrawWireCube(rightZone.position + offset, new Vector3(sideZoneRange, 1, 0));
        Gizmos.DrawWireCube(mainZone.position + offset, new Vector3(mainZoneRange, 1, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(redLineIndicator.transform.position + offset, new Vector3(1, 5, 0));
    }
}
