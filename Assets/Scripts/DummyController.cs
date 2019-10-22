using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    Animator animator;

    // Update is called once per frame
    void Update()
    {
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Damage();
        }
    }
    void Damage()
    {
        animator.SetTrigger("OnDamage");
    }
    void StopAnimation()
    {
        animator.SetTrigger("StopAnimation");
    }
}
