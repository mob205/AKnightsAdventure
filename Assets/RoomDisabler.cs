using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDisabler : MonoBehaviour
{
    public Level startingRoom;

    void Start()
    {
        Level[] rooms = FindObjectsOfType<Level>();
        foreach(Level room in rooms)
        {
            if(room != startingRoom)
            {
                room.gameObject.SetActive(false);
            }
        }
    }

   
}
