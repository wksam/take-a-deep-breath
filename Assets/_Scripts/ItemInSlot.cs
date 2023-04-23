using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInSlot : MonoBehaviour
{
    public ItemData itemData;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void InitializeItem(ItemData itemData)
    {
        this.itemData = itemData;
        image.sprite = this.itemData.Icon;
    }
}
