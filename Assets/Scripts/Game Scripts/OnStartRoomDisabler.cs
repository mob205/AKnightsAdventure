using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartRoomDisabler : MonoBehaviour
{
    public Room startingRoom;

    void Start()
    {
        Room[] rooms = FindObjectsOfType<Room>();
        foreach(Room room in rooms)
        {
            if(room != startingRoom)
            {
                room.gameObject.SetActive(false);
            }
        }
    }

   
}
