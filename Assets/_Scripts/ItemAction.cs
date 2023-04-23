using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAction : ScriptableObject
{
    public abstract bool PerformAction(ItemData data);
}
