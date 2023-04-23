using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComposite : IRoom
{
    List<IRoom> rooms = new List<IRoom>();
    float deltaOxygen = 0;

    public void AddRoom(IRoom room)
    {
        rooms.Add(room);
    }

    public void AddRooms(List<IRoom> rooms)
    {
        foreach (IRoom room in rooms)
        {
            this.rooms.Add(room);
        }
    }

    public void RemoveRoom(IRoom room)
    {
        rooms.Remove(room);
    }

    public IRoom RemoveLast()
    {
        IRoom room = rooms[Count() - 1];
        return room;
    }

    public bool Contains(IRoom room)
    {
        return rooms.Contains(room);
    }

    public int Count()
    {
        return rooms.Count;
    }

    public bool IsEmpty()
    {
        return Count() <= 0;
    }

    public bool PlayerIsHere()
    {
        foreach (Room room in rooms)
        {
            if (room.PlayerIsHere()) return true;
        }
        return false;
    }

    public bool HasFire()
    {
        foreach (Room room in rooms)
        {
            if (room.HasFire) return true;
        }
        return false;
    }

    public override string ToString()
    {
        string s = "";
        foreach (IRoom room in rooms)
        {
            s += room.ToString() + " ";
        }
        return s;
    }

    public void ChangeOxygen(float delta)
    {
        deltaOxygen = Mathf.Max(deltaOxygen + delta, -GetRoomOxygen());
    }

    float GetRoomOxygen()
    {
        float _currentOxygen = 0;
        foreach (IRoom room in rooms)
        {
            _currentOxygen += room.GetCurrentOxygen();
        }
        return _currentOxygen;
    }

    public float GetCurrentOxygen()
    {
        return GetRoomOxygen() + deltaOxygen;
    }

    public int GetArea()
    {
        int _area = 0;
        foreach (IRoom room in rooms)
        {
            _area += room.GetArea();
        }
        return _area;
    }

    public float GetPressure()
    {
        return GetCurrentOxygen() / GetArea();
    }

    public bool HasRoom(Room room)
    {
        foreach (IRoom r in rooms)
        {
            if (r.HasRoom(room))
            {
                return true;
            }
        }
        return false;
    }

    public IRoom Merge(IRoom other)
    {
        if (other is Room)
        {
            rooms.Add(other);
        }
        else
        {
            RoomComposite composite = (RoomComposite)other;
            foreach (Room room in composite.rooms)
            {
                rooms.Add(room);
            }
            deltaOxygen += composite.deltaOxygen;
        }
        return this;
    }

    public List<IRoom> Split()
    {
        List<IRoom> connected = FindConnected();
        List<IRoom> diff = Diff(connected);
        IRoom other = FromList(diff);
        IRoom one = FromList(connected);

        float pressure = GetPressure();
        other.SetPressure(pressure);
        one.SetPressure(pressure);

        if (one.IsEmpty())
            return new List<IRoom>() { other };
        else if (other.IsEmpty())
            return new List<IRoom>() { one };
        return new List<IRoom>() { one, other };
    }

    List<IRoom> Diff(List<IRoom> other)
    {
        List<IRoom> diff = new List<IRoom>();
        foreach (IRoom room in rooms)
        {
            if (!other.Contains(room))
                diff.Add(room);
        }
        return diff;
    }

    IRoom FromList(List<IRoom> rooms)
    {
        if (rooms.Count > 1)
        {
            RoomComposite composite = new RoomComposite();
            foreach (IRoom room in rooms)
            {
                composite.AddRoom(room);
            }
            return composite;
        }
        else if (rooms.Count == 1)
        {
            return rooms[0];
        }
        return null;
    }

    List<IRoom> FindConnected()
    {
        IRoom anchor = rooms[0];
        Stack<IRoom> toCheck = new Stack<IRoom>();
        List<IRoom> visited = new List<IRoom>();

        toCheck.Push(anchor);
        while(toCheck.Count > 0)
        {
            IRoom checking = toCheck.Pop();
            visited.Add(checking);
            foreach (IRoom next in checking.GetNeighbors())
            {
                if (!visited.Contains(next))
                    toCheck.Push(next);
            }
        }
        return visited;
    }

    public List<IRoom> GetNeighbors()
    {
        throw new System.NotImplementedException();
    }

    public void SetPressure(float pressure)
    {
        deltaOxygen = 0;
        foreach (IRoom room in rooms)
        {
            room.SetPressure(pressure);
        }
    }
}
