using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea]
    public string[] dialogs;

    [SerializeField] DialogBox dialogBox;
    [SerializeField] bool skippable;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!inRange) { return; }
        if (Input.GetKeyDown(KeyCode.E) && dialogBox.inDialog == false)
        {
            dialogBox.PlayDialog(dialogs, skippable);
        }
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
