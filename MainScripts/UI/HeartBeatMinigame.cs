using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class HeartBeatMinigame : MonoBehaviour
{
    [Header("Debug:")]
    [SerializeField] public int score = 0;
    [SerializeField] public float time = 0f;
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
    public AudioClip triggerHBSFX;
    public AudioClip spaceBarDownSFX;
    public AudioSource audioSource;
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
        redLineIndicator.transform.position = resetPoint.position;
        StopAllCoroutines();
        StartCoroutine(HeartBeatEvent());
    }
    public void HeartBeatEnd()
    {
        redLineIndicator.transform.position = resetPoint.position;
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
public IEnumerator HeartBeatEvent()
    {
        time = 0f;
        Image redlineimg = redLineIndicator.GetComponent(typeof(Image)) as Image;
        StartCoroutine(Timer());
        while (time < length)
        {
            float cooldown = Random.Range(minCooldown, maxCooldown);
            yield return new WaitForSeconds(cooldown);
            Debug.Log("Finished Cooldown: " + (Mathf.Round(cooldown * 100) / 100) + "s");
            audioSource.PlayOneShot(triggerHBSFX);
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
                MinigameManger.HeartMiniGame();
                MinigameManger.progressionWin();
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
                audioSource.PlayOneShot(spaceBarDownSFX);
            }
            heartBeat.fillAmount += (5 * speed/ 250) / (5*speed*Time.timeScale);
            redLineIndicator.transform.localPosition += new Vector3(5*speed*Time.timeScale,0,0);
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
        if (redLineIndicator.transform.localPosition.x == mainZone.localPosition.x || (redLineIndicator.transform.localPosition.x < (mainZone.localPosition.x + mainZoneRange) && redLineIndicator.transform.localPosition.x > (mainZone.localPosition.x - mainZoneRange)))
        {
            score += mainZonePointGain;
            time += mainZonePointGain;
            Debug.Log("Hit Main zone, Adding " + mainZonePointGain + " point/s. New point total of: " + score);
        }
        else if (redLineIndicator.transform.localPosition.x == leftZone.localPosition.x || (redLineIndicator.transform.localPosition.x < (leftZone.localPosition.x + sideZoneRange) && redLineIndicator.transform.localPosition.x > (leftZone.localPosition.x - sideZoneRange)) || redLineIndicator.transform.localPosition.x == rightZone.localPosition.x || (redLineIndicator.transform.localPosition.x < (rightZone.localPosition.x + sideZoneRange) && redLineIndicator.transform.localPosition.x > (rightZone.localPosition.x - sideZoneRange)))
        {
            score += sideZonePointGain;
            time += sideZonePointGain;
            Debug.Log("Hit Side zone, Adding " + sideZonePointGain + " point/s. New point total of: " + score);
        }
        else
        {
            score += missedZonesPointLoss;
            time += missedZonesPointLoss;
            Debug.Log("Missed Zones, Removing " + missedZonesPointLoss + " point/s. New point total of: " + score);
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 0, 0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(leftZone.position + offset, new Vector3(sideZoneRange/20,1,0));
        Gizmos.DrawWireCube(rightZone.position + offset, new Vector3(sideZoneRange/20, 1, 0));
        Gizmos.DrawWireCube(mainZone.position + offset, new Vector3(mainZoneRange/20, 1, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(redLineIndicator.transform.position + offset, new Vector3(1, 5, 0));
    }
}
