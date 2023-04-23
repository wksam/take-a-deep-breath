using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tipText;
    List<IInteractable> _interactablesInRange = new List<IInteractable>();
    List<IInteractable> _noninteractablesInRange = new List<IInteractable>();
    PlayerInput _playerInput;

    void Awake()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
    }

    void Update()
    {
        ShowHideTip();
        OnInteract();
    }

    void ShowHideTip()
    {
        if (_interactablesInRange.Count > 0)
        {
            tipText.text = "Press [E] to " + _interactablesInRange[0].InteractActionText();
        }
        else
        {
            tipText.text = "";
        }
    }

    void OnInteract()
    {
        if (_playerInput != null && _playerInput.actions["Interact"].triggered && _interactablesInRange.Count > 0)
        {
            IInteractable interactable = _interactablesInRange[0];
            interactable.Interact();
            if (!interactable.CanInteract())
            {
                _interactablesInRange.Remove(interactable);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactable.CanInteract())
        {
            _interactablesInRange.Add(interactable);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        var interactable = other.GetComponent<IInteractable>();
        if (_interactablesInRange.Contains(interactable))
        {
            _interactablesInRange.Remove(interactable);
        }
    }
}
