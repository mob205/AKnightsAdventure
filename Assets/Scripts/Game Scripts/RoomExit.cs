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

    public bool fadeOnChange;
    public float fadeSpeed = 1f;
    public float fadeDuration = 2f;
    public float switchDuration = 0f;

    public bool isDoor;

    private PlaceCard placeCard;
    private new CameraController camera;

    private void Start()
    {
        placeCard = FindObjectOfType<PlaceCard>();
        camera = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDoor)
        {
            SwitchLevels();
        }
    }
    public void SwitchLevels()
    {
        if (fadeOnChange) { FadeToBlack.instance.Fade(fadeSpeed, fadeDuration); }
        if (currentRoom.roomName != nextRoom.roomName)
        {
            placeCard.Activate(nextRoom.roomName);
        }
        StartCoroutine(DelayedLevelChange());
        
    }
    IEnumerator DelayedLevelChange()
    {
        yield return new WaitForSeconds(switchDuration);
        camera.ChangeView(nextRoom.cameraBounds);
        PlayerMovement.instance.transform.position += (Vector3)playerChange;
    }
}

