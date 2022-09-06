using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Variables : MonoBehaviour
{
    public bool exitRequirements = false;

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame) exitRequirements = !exitRequirements;
    }
}
