using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI displayText;

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
        displayText.text = text;
        //Time.timeScale = 0;
        //StartCoroutine("DisplayByChar", text);
    }
    //IEnumerator DisplayByChar(string text)
    //{
    //    for (int i = 0; i < text.Length; i++)
    //    {
    //        displayText.text += text[i];
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //    Debug.Log("Done");
    //}
}
