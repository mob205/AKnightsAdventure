using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExit : MonoBehaviour
{
    [SerializeField] Vector2 playerChange;

    public float disableDelay;
    public Level currentLevel;
    public Level nextLevel;
    
    private new CameraController camera;

    private void Start()
    {
        camera = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadLevels();
            camera.ChangeView(nextLevel.cameraBounds);
            collision.transform.position += (Vector3)playerChange;
        }
    }
    void LoadLevels()
    {
        nextLevel.Enable();
        currentLevel.Disable(disableDelay);
    }
}

