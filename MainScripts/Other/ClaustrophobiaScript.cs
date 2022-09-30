using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClaustrophobiaScript : MonoBehaviour
{
    public float length = 120f;
    public float speed = 5f;
    public GameObject SoundEmitter;
    [SerializeField] private float time;

    public void StartClaus()
    {
        transform.localPosition = new Vector3(0, 10, 0);
        transform.localScale = new Vector3(1000, 1000, 1000);
        StopAllCoroutines();
        StartCoroutine(shrinkAni());
        SoundEmitter.SetActive(true);
    }

    public void StopClaus()
    {
        StopAllCoroutines();
        SoundEmitter.SetActive(false);
        transform.localPosition = new Vector3(0, 10, 0);
        transform.localScale = new Vector3(1000, 1000, 1000);

    }
    public IEnumerator shrinkAni()
    {
        time = 0f;
        while (time < length)
        {
            time += Time.deltaTime;
            transform.localScale -= new Vector3((transform.localScale.x / length) * speed * Time.deltaTime, (transform.localScale.y / length) * speed * Time.deltaTime, (transform.localScale.z / length) * speed * Time.deltaTime);
            transform.localPosition -= new Vector3(0, (transform.localPosition.y / length) * speed * Time.deltaTime, 0);
            yield return null;  
        }
    }
}
