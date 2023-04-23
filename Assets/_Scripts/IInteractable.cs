using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractActionText();
    public void Interact();
    public bool CanInteract();
}
