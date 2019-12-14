using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RoomExit : MonoBehaviour
{
    [SerializeField] Vector2 playerChange;

    public float disableDelay;
    public Room currentRoom;
    public Room nextRoom;

    private PlaceCard placeCard;
    private new CameraController camera;

    private void Start()
    {
        placeCard = FindObjectOfType<PlaceCard>();
        camera = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadLevels();
            camera.ChangeView(nextRoom.cameraBounds);
            collision.transform.position += (Vector3)playerChange;
        }
    }
    void LoadLevels()
    {
        nextRoom.Enable();
        //currentRoom.Disable(disableDelay);
        if (currentRoom.roomName != nextRoom.roomName)
        {
            placeCard.Activate(nextRoom.roomName);
        }
    }
}

