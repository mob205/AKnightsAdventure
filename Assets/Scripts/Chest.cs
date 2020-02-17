using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : Interactable
{
    public UnityEvent interactEvent;
    public Sprite openChest;

    private bool hasOpened;

    protected override void Interact()
    {
        if (!hasOpened)
        {
            base.Interact();
            interactEvent.Invoke();
            hasOpened = true;
            spriteRenderer.sprite = openChest;
        }
    }
}
