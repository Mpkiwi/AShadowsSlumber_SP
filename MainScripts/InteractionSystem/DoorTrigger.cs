using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable
{
    [Header("Door Requirements:")]
    [SerializeField] private string prompt;
    [TextArea]
    public string Note = "Don't be stupid future me, people see this when playing the game";

    public Transform player;

    public Door door1;

    public Door door2;

    [TextArea]
    public string ImportantNote = "Only use door 2 if it is a double door";

    public string InteractionPrompt { get; }
    public string InteractPrompt => prompt;
    public void Interact(Interactor interactor)
    {
        door1.ToggleDoor(player.position);
        if (door2)
        {
            door2.ToggleDoor(player.position);
        }
        Debug.Log("Player.Toggle.Door");
    }
}