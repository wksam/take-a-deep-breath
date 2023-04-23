using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public static event Action<List<Room>> OnDoorOpened;
    public static event Action<List<Room>> OnDoorClosed;
    public bool IsOpen { get; private set; }
    [SerializeField] bool _isLocked;
    [SerializeField] bool _requiredKeycard;
    [SerializeField] SpriteRenderer _leftDoorSprite, _rightDoorSprite;
    [SerializeField] string _openDoorTipText = "open the door";
    [SerializeField] string _closeDoorTipText = "close the door";
    [SerializeField] List<Room> _connectedRooms;
    Animator _animator;
    public List<Room> ConnectedRooms => _connectedRooms;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        IsOpen = _animator.GetBool("open");
    }

    void Start()
    {
        if (_isLocked || _requiredKeycard)
            _leftDoorSprite.color = _rightDoorSprite.color = new Color(255/255f, 76/255f, 93/255f);
        else
            _leftDoorSprite.color = _rightDoorSprite.color = new Color(0/255f, 164/255f, 117/255f);
    }

    public void Interact()
    {
        IsOpen = !_animator.GetBool("open");
        _animator.SetBool("open", IsOpen);
    
        if (IsOpen) OnDoorOpened.Invoke(_connectedRooms);
        else OnDoorClosed.Invoke(_connectedRooms);
    }

    public bool CanInteract()
    {
        return !_isLocked && !_requiredKeycard;
    }

    public string InteractActionText()
    {
        if (!IsOpen) return _openDoorTipText;
        else return _closeDoorTipText;
    }
}
