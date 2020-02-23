using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [TextArea] public string[] dialogs;
    DialogBox dialogBox;
    [SerializeField] bool skippable;
    [SerializeField] bool triggerInteract;

    public UnityEvent interactEvent;

    private bool inRange;
    protected SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    protected void Start()
    {
        dialogBox = DialogBox.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!inRange || triggerInteract) { return; }
        if (Input.GetKeyDown(KeyCode.E) && dialogBox.inDialog == false)
        {
            Interact();
        }
    }
    protected virtual void Interact()
    {
        if(interactEvent != null)
        {
            interactEvent.Invoke();
        }
        if(dialogs.Length != 0)
        {
            dialogBox.PlayDialog(dialogs, skippable);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            if (triggerInteract) { Interact(); }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
