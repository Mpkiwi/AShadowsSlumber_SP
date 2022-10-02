using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public levelLoader lLoadS;
    public AudioSource mmAudioSource;
    public AudioClip soundEGG;
    public void Play()
    {
        lLoadS.LoadScene(1);
    }
    public void Quit()
    {
        Debug.Log("GameShuttingDown.User.Exit");
        Application.Quit();
    }
    public void MenuEE()
    {
        mmAudioSource.PlayOneShot(soundEGG);
    }
}
