using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.PostProcessing;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PartyMode : MonoBehaviour
{
    public PartyModeSo partyModeSo;
    public ColorAdjustments colorAdjComp;
    public static bool partyAnimal = false;
    private void Awake()
    {
        Volume volume = gameObject.GetComponent<Volume>();
        ColorAdjustments colorAdj;
        if (volume.sharedProfile.TryGet<ColorAdjustments>(out colorAdj))
        {
            colorAdjComp = colorAdj;
        }
        if (partyModeSo.partyAnimal == true)
        {
            colorAdjComp.hueShift.value = 0;
            StopAllCoroutines();
            StartCoroutine(discoMode());

        }

    }
    
    public void PartyToggle()
    {
        if (partyModeSo.partyAnimal)
        {
            partyModeSo.partyAnimal = false;
            colorAdjComp.hueShift.value = 0;
            StopAllCoroutines();
        }
        else if (!partyModeSo.partyAnimal)
        {
            partyModeSo.partyAnimal = true;
            colorAdjComp.hueShift.value = 0;
            StopAllCoroutines();
            StartCoroutine(discoMode());
        }
    }
    public void PartyStarter()
    {
        StartCoroutine(discoMode());
    }
    public void PartyCrasher()
    {
        StopAllCoroutines();
    }
    public IEnumerator discoMode()
    {
        int i = 0;
        while (partyModeSo.partyAnimal == true)
        { 
            colorAdjComp.hueShift.value = i;
            if (i < 180)
            {
                i++;
            }
            else if(i >= 180)
            {
                i = -180; 
            }
            yield return null;
        }
    }
}