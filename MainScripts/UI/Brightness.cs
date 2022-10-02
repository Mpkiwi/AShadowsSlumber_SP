using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.PostProcessing;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Brightness : MonoBehaviour
{
    public Slider slider;

    public ColorAdjustments colorAdjComp;
    private void Start()
    {
        Volume volume = gameObject.GetComponent<Volume>();
        ColorAdjustments colorAdj;
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdj))
        {
            colorAdjComp = colorAdj;
            ChangeBrightness(PlayerPrefs.GetFloat("videoGamma"));
        }
    }
    public void ChangeBrightness(float brightnessValue)
    {
        PlayerPrefs.SetFloat("videoGamma", brightnessValue);
        Debug.Log("Player.Brightness = " + brightnessValue * 0.368421 + -5.36842);
        colorAdjComp.postExposure.value = ((float)(brightnessValue * 0.368421 + -5.36842)); 
        //Note: Future ref for future me from past this is a linear line relation ship, for figuring stuff like this out in the future plot the input to the output like coordinates and then solve the line equation to get you values needed for your calculations.
    }
}
