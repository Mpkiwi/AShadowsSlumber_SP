using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public levelLoader lLoadS;
    public MinigameManger manger;   
    public string InteractionPrompt { get; }
    public string InteractPrompt => prompt;

    public void Interact(Interactor interactor)
    {
        manger.progressionCheck();
        var varibles = interactor.GetComponent<Variables>();
        if (varibles.exitRequirements)
        {
            lLoadS.LoadScene(0);
            Debug.Log("Opening Exit Door!");
        }
        else
        {
            Debug.Log("Requiremnts to leave not met");
        }
    }
}
