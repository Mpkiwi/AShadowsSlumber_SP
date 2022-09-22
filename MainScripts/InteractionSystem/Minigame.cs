using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour, IInteractable
{
    [SerializeField] public MinigameManger minigameManger;

    [SerializeField] private string prompt;

    public string InteractionPrompt { get; }
    public string InteractPrompt => prompt;
        public void Interact(Interactor interactor)
        {
            minigameManger.HeartMiniGame();
            Debug.Log("Starting Heatbeat Minigame");
            /*float game = Mathf.Round(Random.Range(0f, 1f));
            if (game == 0)
            {
                minigameManger.HeartMiniGame();
                Debug.Log("Starting Heatbeat Minigame");
            }
            if (game == 1)
            {
                minigameManger.BreathMiniGame();
                Debug.Log("Starting Breathing Minigame");
            }*/
        }
}
