using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PartyModeSo : ScriptableObject
{
	[SerializeField] private bool partyAnimal_ = false;
 
    public bool partyAnimal
    {
        get { return partyAnimal_; }
        set { partyAnimal_ = value; }
    }
}
