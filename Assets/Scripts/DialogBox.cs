using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{
    [SerializeField] int printSpeed = 10;
    [SerializeField] TextMeshProUGUI displayText;

    public bool inDialog;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayDialog(string text)
    {
        gameObject.SetActive(true);
        displayText.text = "";
        Time.timeScale = 0;
        inDialog = true;
        StartCoroutine("DisplayByChar", text);
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
        FinishDialog();
    }
    private void FinishDialog()
    {
        Time.timeScale = 1;
        inDialog = false;
        gameObject.SetActive(false);
    }
}
