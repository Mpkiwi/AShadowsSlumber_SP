using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour, IInteractable
{
    [SerializeField] public MinigameManger minigameManger;

    [SerializeField] private string prompt;

    public float game;
    public string InteractionPrompt { get; }
    public string InteractPrompt => prompt;

    public void Interact(Interactor interactor)
    {
        minigameManger.gameTrigger();
        if (!minigameManger.gameActive)
        { 
            game = Mathf.Round(Random.Range(0f, 1f));
            if (game == 0)
            {
                minigameManger.HeartMiniGame();
                Debug.Log("Starting Heatbeat Minigame");
            }
            if (game == 1)
            {
                minigameManger.BreathMiniGame();
                Debug.Log("Starting Breathing Minigame");
            }
        }
        else if(minigameManger.gameActive)
        {
            if (game == 0)
            {
                minigameManger.HeartMiniGame();
                Debug.Log("Stopping Heartbeat Minigame");
            }
            if (game == 1)
            {
                minigameManger.BreathMiniGame();
                Debug.Log("Stopping Breathing Minigame");
            }
        }
    }
}
