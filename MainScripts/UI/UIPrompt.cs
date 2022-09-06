using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPrompt : MonoBehaviour
{
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private TextMeshProUGUI textPrompt;

    private void Start()
    {
        UIPanel.SetActive(false);
    }
    public bool displayed = false;
    public void SetUp(string PromptText)
    {
        textPrompt.text = PromptText;
        UIPanel.SetActive(true);
        displayed = true;
    }
    public void Close()
    {
        UIPanel.SetActive(false); 
        displayed = false; 
    }
}
