using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    public string DisplayName;
    public Sprite Icon;
    public GameObject ItemPrefab;
    public ItemAction Action;
}
