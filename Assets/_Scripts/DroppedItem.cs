using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable
{
    [SerializeField] bool _canInteract = true;
    Inventory _inventory;
    public ItemData itemData;

    void Awake()
    {
        _inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    public bool CanInteract()
    {
        return _canInteract;
    }

    public void Interact()
    {
        if (_inventory.AddItem(itemData))
        {
            Destroy(gameObject);
        }
    }

    public string InteractActionText()
    {
        return $"get the {itemData.DisplayName}";
    }
}
