using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Slot> _slots;
    [SerializeField] GameObject _itemInSlotPrefab;
    int _selectedSlot = 0;
    PlayerInput _playerInput;
    InputAction _slotAction;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _slotAction = _playerInput.actions["slots"];
    }

    void Start()
    {
        ChangeSelectedSlot(0);
    }

    void Update()
    {
        if (_slotAction.triggered)
        {
            ChangeSelectedSlot((int)_slotAction.ReadValue<float>());
        }
    }

    void OnUse()
    {
        UseSelectedItem();
    }

    void OnDrop()
    {
        DropSelectedItem();
    }

    void ChangeSelectedSlot(int newSelectedSlot)
    {
        _slots[_selectedSlot].DeselectItem();
        _slots[newSelectedSlot].SelectItem();
        _selectedSlot = newSelectedSlot;
    }

    public bool AddItem(ItemData item)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.transform.childCount > 0) continue;
            GameObject newItem = Instantiate(_itemInSlotPrefab, slot.transform);
            ItemInSlot itemInSlot = slot.GetComponentInChildren<ItemInSlot>();
            itemInSlot.InitializeItem(item);
            return true;
        }
        return false;
    }

    void UseSelectedItem()
    {
        ItemInSlot itemInSlot = _slots[_selectedSlot].GetComponentInChildren<ItemInSlot>();
        if (itemInSlot != null)
        {
            if (itemInSlot.itemData.Action.PerformAction(itemInSlot.itemData))
            {
                Destroy(itemInSlot.gameObject);
            }
        }
    }

    void DropSelectedItem()
    {
        ItemInSlot itemInSlot = _slots[_selectedSlot].GetComponentInChildren<ItemInSlot>();
        if (itemInSlot != null)
        {
            Instantiate(itemInSlot.itemData.ItemPrefab, transform.position, Quaternion.identity);
            Destroy(itemInSlot.gameObject);
        }
    }
}
