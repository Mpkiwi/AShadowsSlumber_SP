using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenceScript : MonoBehaviour
{
    public static int isAgreed;

    private void Start()
    {
        PlayerPrefs.GetInt("isAgreed");
        if(isAgreed == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
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
