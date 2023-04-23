using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    [SerializeField] SetupData _setupData;
    [SerializeField] Slider _slider;
    [SerializeField] float nominalOxygen = 1000f;
    RoomManager roomManager;
    List<IRoom> rooms;
    float elapsed = 0f;
    float nextUpdateInSecond = 1f;

    void Awake()
    {
        roomManager = GetComponent<RoomManager>();
    }

    void Start()
    {
        rooms = new List<IRoom>();
        foreach (Room room in roomManager.Rooms) rooms.Add(room);
        UpdateHUD();
    }

    void OnEnable()
    {
        DoorController.OnDoorOpened += CombineRooms;
        DoorController.OnDoorClosed += UncombineRooms;
    }

    void OnDisable()
    {
        DoorController.OnDoorOpened -= CombineRooms;
        DoorController.OnDoorClosed -= UncombineRooms;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= nextUpdateInSecond)
        {
            elapsed %= nextUpdateInSecond;
            UpdateOxygenLevels();
        }
    }

    void CombineRooms(List<Room> connectedRooms)
    {
        IRoom room1 = rooms.Find((r) => r.HasRoom(connectedRooms[0]));
        IRoom room2 = rooms.Find((r) => r.HasRoom(connectedRooms[1]));

        IRoom merged = room1.Merge(room2);

        rooms.Remove(room1);
        rooms.Remove(room2);
        rooms.Add(merged);

        UpdateHUD();
    }

    void UncombineRooms(List<Room> connectedRooms)
    {
        IRoom room = rooms.Find((r) => r.HasRoom(connectedRooms[0]));
        List<IRoom> splitted = room.Split();
        rooms.Remove(room);

        foreach (IRoom r in splitted)
        {
            rooms.Add(r);
        }
        
        UpdateHUD();
    }
    void UpdateOxygenLevels()
    {
        foreach (IRoom room in rooms)
        {
            if (room.PlayerIsHere())
            {
                room.ChangeOxygen(-_setupData.BreathingRatePerSecond);
            }
        }
        UpdateHUD();
    }

    void UpdateHUD()
    {
        _slider.value = Math.Min(CurrentOxygenLevel() / nominalOxygen, 1);
    }

    float CurrentOxygenLevel()
    {
        foreach (IRoom room in rooms)
        {
            if (room.PlayerIsHere())
            {
                return room.GetCurrentOxygen();
            }
        }
        return -1;
    }
}
