using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Room")]
public class RoomData : ScriptableObject
{
    public string DisplayName;
    public float StartOxygenLevel;
    public int Area;
    public float TotalOxygen;
    public SetupData SetupData;

    void OnEnable()
    {
        DisplayName = name;
        TotalOxygen = Area * SetupData.OxygenPerSquare;
        StartOxygenLevel = Random.Range(42, (int)(TotalOxygen * 0.5f));
        // StartOxygenLevel = TotalOxygen * .5f;
    }
}
