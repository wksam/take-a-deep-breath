using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] List<Room> _rooms;
    public List<Room> Rooms => _rooms;
}
