using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

class CreditsScript : MonoBehaviour
{
    public TMPro.TMP_Text CreditsText;

    void Awake()
    {
        StartCoroutine(GrabCredits());
    }
    IEnumerator GrabCredits()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/Mpkiwi/AShadowsSlumber_SP/main/UnityTextDocs/Credits.txt");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            CreditsText.text = www.downloadHandler.text;
        }
    }
}