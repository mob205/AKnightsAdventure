using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public Sprite openChest;

    private bool hasOpened;

    AudioSource audioSource; 

    new public void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }
    protected override void Interact()
    {
        if (!hasOpened)
        {
            base.Interact();
            SetOpen();
            audioSource.Play();
        }
    }
    public void SetOpen()
    {
        hasOpened = true;
        spriteRenderer.sprite = openChest;
    }
}
