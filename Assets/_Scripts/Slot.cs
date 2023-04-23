using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Color _selectedColor;
    [SerializeField] Color _deselectedColor;
    Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SelectItem()
    {
        _image.color = _selectedColor;
    }

    public void DeselectItem()
    {
        _image.color = _deselectedColor;
    }
}
