using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{
    [SerializeField] int printSpeed = 10;
    [SerializeField] TextMeshProUGUI displayText;

    [HideInInspector] public bool inDialog;

    private bool isSkippable;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isSkippable)
        {
            FinishDialog();
        }
    }
    public void PlayDialog(string[] dialogs, bool skippable)
    {
        isSkippable = skippable;
        gameObject.SetActive(true);
        displayText.text = "";
        Time.timeScale = 0;
        inDialog = true;
        StartCoroutine("DisplayStrings", dialogs);
    }
    IEnumerator DisplayStrings(string[] dialogs)
    {
        foreach(string dialog in dialogs)
        {
            yield return StartCoroutine("DisplayByChar", dialog);
            displayText.text = "";
        }
        FinishDialog();
    }
    IEnumerator DisplayByChar(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            displayText.text += text[i];
            yield return new WaitForSeconds(1/printSpeed);
        }
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }
    }
    private void FinishDialog()
    {
        Time.timeScale = 1;
        inDialog = false;
        gameObject.SetActive(false);
    }
}
