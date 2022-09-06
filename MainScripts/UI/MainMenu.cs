using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public levelLoader lLoadS;

    public void Play()
    {
        lLoadS.LoadNextScene(1);
    }

    public void Quit()
    {
        Debug.Log("GameShuttingDown.User.Exit");
        Application.Quit();
    }
}
