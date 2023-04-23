using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Room currentRoom;
    public IRoom GetCurrentRoom() => currentRoom;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Room")
        {
            currentRoom.SetPlayerIsHere(false);
            currentRoom = other.GetComponent<Room>();
            currentRoom.SetPlayerIsHere(true);
        }
    }
}
