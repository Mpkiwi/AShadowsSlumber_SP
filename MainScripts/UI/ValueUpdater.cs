using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueUpdater : MonoBehaviour
{
    public Slider slider;
    public TMPro.TMP_InputField field;

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
        field.onValueChanged.AddListener(OnFieldChanged);
    }

    private void OnSliderChanged(float number)
    {
        if (field.text != number.ToString())
        {
            number = Mathf.Round(number);
            field.text = number.ToString();
        }
    }

    private void OnFieldChanged(string text)
    {
        if (slider.value.ToString() != text)
        {
            if (float.TryParse(text, out float number))
            {
                slider.value = number;
            }
        }
    }
}