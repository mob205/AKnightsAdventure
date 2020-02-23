using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public Sprite openChest;

    private bool hasOpened;

    protected override void Interact()
    {
        if (!hasOpened)
        {
            base.Interact();
            SetOpen();
        }
    }
    public void SetOpen()
    {
        hasOpened = true;
        spriteRenderer.sprite = openChest;
    }
}
