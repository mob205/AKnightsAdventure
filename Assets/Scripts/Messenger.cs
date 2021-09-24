using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Messenger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;

    public static Messenger instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Message(string message, Color color)
    {
        textBox.text = message;
        textBox.color = color;
    }
    public void Message(string message, Color color, float duration)
    {
        Message(message, color);
        Invoke("ClearMessage", duration);
    }
    void ClearMessage()
    {
        textBox.text = "";
    }

}
