using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceCard : MonoBehaviour
{
    private TextMeshProUGUI placeText;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        placeText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Activate(string text)
    {
        placeText.text = text;
        animator.SetTrigger("Activate");
    }
}
