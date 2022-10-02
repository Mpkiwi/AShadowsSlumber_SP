using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Teleport;

public class Teleport : MonoBehaviour, IInteractable
{
    public AudioClip teleportSFX;
    public AudioSource audioSource;

    [SerializeField] public levelLoader lLoadS;

    [SerializeField] public int levelNumber;

    [SerializeField] private string prompt;

    public string InteractionPrompt { get; }
    public string InteractPrompt => prompt;
    public void Interact(Interactor interactor)
    {
        audioSource.PlayOneShot(teleportSFX);
        Debug.Log("Teleporting Player!");
        lLoadS.LoadScene(levelNumber);
    }
}