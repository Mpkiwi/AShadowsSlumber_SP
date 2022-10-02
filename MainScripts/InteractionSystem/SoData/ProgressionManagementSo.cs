using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProgressionManagementSo : ScriptableObject
{
	[SerializeField] private bool clausGameComplete_ = false;
    [SerializeField] private bool darknessGameComplete_ = false;
    public bool clausGameComplete
    {
		get { return clausGameComplete_; }
		set { clausGameComplete_ = value; }
	}
    public bool darknessGameComplete
    {
        get { return darknessGameComplete_; }
        set { darknessGameComplete_ = value; }
    }

}
