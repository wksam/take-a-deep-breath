using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoom
{
    bool PlayerIsHere();
    void SetPressure(float pressure);
    int Count();
    bool IsEmpty();
    List<IRoom> GetNeighbors();
    List<IRoom> Split();
    IRoom Merge(IRoom other);
    bool HasRoom(Room room);
    int GetArea();
    float GetPressure();
    float GetCurrentOxygen();
    void ChangeOxygen(float delta);
}
