using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public bool IsOpen = false;

    [Header("Required Refs:")]
    public Transform pivot;
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    [SerializeField] AudioSource audioSource;

    [Header("Configs:")]
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private float ForwardDirection = 0;
    [SerializeField] private float Speed = 1f;

    private Vector3 StartRotation;
    private Vector3 StartPosition;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = pivot.rotation.eulerAngles;
        Forward = transform.forward;
        StartPosition = pivot.position;
    }

    public void ToggleDoor(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            Open(UserPosition);
        }
        else if (IsOpen)
        {
            Close();
        }
    }   
    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            if (openSound != null)
            {
                audioSource.PlayOneShot(openSound);
            }
            float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
            Debug.Log($"Dot: {dot.ToString("N3")}");
            AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = pivot.rotation;
        Quaternion endRotation;

        if (ForwardAmount >= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            pivot.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            if (closeSound != null)
            {
                audioSource.PlayOneShot(closeSound);
            }
            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = pivot.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            pivot.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}