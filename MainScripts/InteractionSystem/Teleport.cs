using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Teleport;

public class Teleport : MonoBehaviour, IInteractable
{
    [SerializeField] public levelLoader lLoadS;

    [SerializeField] public int levelNumber;

    [SerializeField] private string prompt;

    public string InteractionPrompt { get; }
    public string InteractPrompt => prompt;
        public void Interact(Interactor interactor)
        {   
            Debug.Log("Teleporting Player!");
            lLoadS.LoadNextScene(levelNumber);
        }
}
