using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea]
    public string[] dialogs;

    DialogBox dialogBox;

    [SerializeField] bool skippable;
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
        if(!inRange) { return; }
        if (Input.GetKeyDown(KeyCode.E) && dialogBox.inDialog == false)
        {
            Interact();
        }
    }
    protected virtual void Interact()
    {
            dialogBox.PlayDialog(dialogs, skippable);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
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
