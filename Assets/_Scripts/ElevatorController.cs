using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour, IInteractable
{
    [SerializeField] GameEvent _elevatorUp;
    [SerializeField] GameEvent _elevatorDown;
    [SerializeField] bool _isGoingUp;
    [SerializeField] string _goUpTipText = "go up";
    [SerializeField] string _goDownTipText = "go down";

    public bool CanInteract()
    {
        return true;
    }

    public string InteractActionText()
    {
        if (_isGoingUp) return _goUpTipText;
        else return _goDownTipText;
    }

    public void Interact()
    {
        // OnElevatorUpInteraction.Invoke(GoingUp);
        if (_isGoingUp) _elevatorUp.Raise();
        else _elevatorDown.Raise();
    }
}
