using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Damage();
        }
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Damage()
    {
        animator.Play("Damaged");
    }
}
