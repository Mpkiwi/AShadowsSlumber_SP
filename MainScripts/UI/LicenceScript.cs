using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenceScript : MonoBehaviour
{
    public static int isAgreed;
    private GameObject LicenceMenu;

    private void Awake()
    {
        PlayerPrefs.GetInt("isAgreed");
        if(isAgreed == 1)
        {
            LicenceMenu.SetActive(false);
        }
        else 
        {
            LicenceMenu.SetActive(true);
        }
    }
    public void accept()
    {
        PlayerPrefs.SetInt("isAgreed", 1);
    }
    public void reject()
    {
        PlayerPrefs.SetInt("isAgreed", 0);
    }
}
