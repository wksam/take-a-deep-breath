using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, IRoom
{
    [SerializeField] RoomData roomData;
    [SerializeField] List<DoorController> doors;
    [SerializeField] bool _playerIsHere;
    public List<DoorController> Doors => doors;
    public bool HasFire;
    float _currentOxygen;

    void Awake()
    {
        _currentOxygen = roomData.StartOxygenLevel;
    }

    public List<Room> AllConnectedRooms()
    {
        Stack<Room> toCheck = new Stack<Room>();
        List<Room> visited = new List<Room>();

        toCheck.Push(this);
        while(toCheck.Count > 0)
        {
            Room checking = toCheck.Pop();
            visited.Add(checking);
            foreach (DoorController door in checking.Doors)
            {
                if (!door.IsOpen) continue;
                if (!visited.Contains(door.ConnectedRooms[0])) toCheck.Push(door.ConnectedRooms[0]);
                if (!visited.Contains(door.ConnectedRooms[1])) toCheck.Push(door.ConnectedRooms[1]);
            }
        }
        return visited;
    }

    public void ChangeOxygen(float delta)
    {
        _currentOxygen = Mathf.Max(_currentOxygen + delta, 0);
    }

    public float GetCurrentOxygen()
    {
        return _currentOxygen;
    }

    public int GetArea()
    {
        return roomData.Area;
    }

    public float GetPressure()
    {
        return GetCurrentOxygen() / GetArea();
    }

    public bool HasRoom(Room room)
    {
        return room == this;
    }

    public IRoom Merge(IRoom other)
    {
        RoomComposite composite = new RoomComposite();
        composite.AddRoom(this);
        return composite.Merge(other);
    }

    public List<IRoom> Split()
    {
        return new List<IRoom>() { this };
    }

    public List<IRoom> GetNeighbors()
    {
        List<IRoom> rooms = new List<IRoom>();
        foreach (DoorController door in Doors)
        {
            if (door.ConnectedRooms[0] == this && door.IsOpen)
            {
                rooms.Add(door.ConnectedRooms[1]);
            }
            else if (door.ConnectedRooms[1] == this && door.IsOpen)
            {
                rooms.Add(door.ConnectedRooms[0]);
            }
        }
        return rooms;
    }

    public int Count()
    {
        return 1;
    }

    public bool IsEmpty()
    {
        return false;
    }

    public void SetPressure(float pressure)
    {
        _currentOxygen = pressure * GetArea();
    }

    public bool PlayerIsHere()
    {
        return _playerIsHere;
    }

    public void SetPlayerIsHere(bool value)
    {
        _playerIsHere = value;
    }
}
