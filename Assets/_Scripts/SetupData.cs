using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Setup")]
public class SetupData : ScriptableObject
{
    public float OxygenPerSquare;
    public float BreathingRatePerSecond;
    public float FireRatePerSecond;
}
