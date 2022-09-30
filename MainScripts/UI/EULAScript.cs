using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

class EULAScript : MonoBehaviour
{
    public TMPro.TMP_Text EulaText;

    void Awake()
    {
        StartCoroutine(GrabEula());
    }
    IEnumerator GrabEula()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/Mpkiwi/AShadowsSlumber_SP/main/UnityTextDocs/EULA.txt");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            EulaText.text = www.downloadHandler.text;
        }
    }
}