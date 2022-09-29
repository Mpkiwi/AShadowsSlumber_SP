using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    [SerializeField] public Animator transition;

    [SerializeField] public float time = 1f;

    public MinigameManger Minigames;

    public void LoadScene(int build) 
    {
        StartCoroutine(Loadlevel(build));
    }

    IEnumerator Loadlevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(levelIndex);
    }
}
