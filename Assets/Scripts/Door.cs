using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [TextArea] public string[] lockedDialogs;

    protected override void Interact()
    {
        if (!PlayerMovement.instance.hasKey)
        {
            dialogBox.PlayDialog(lockedDialogs, skippable);
            return;
        }
        base.Interact();
        FadeToBlack.instance.Fade(5f, 99999);
        
    }
}
