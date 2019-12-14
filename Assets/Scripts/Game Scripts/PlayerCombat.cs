﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float attackCD = 0.5f;
    [SerializeField] int damage = 5;
    [SerializeField] int knockback = 5;

    [HideInInspector]
    public static PlayerCombat instance;

    private Animator animator;
    private bool canAttack = true;

    void Start()
    {
        instance = this;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        ProcessAttacks();
    }
    private void ProcessAttacks()
    {
        if (!canAttack) { return; }
        if (PlayerMovement.instance.isKnockbacked) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            PlayerMovement.instance.ToggleMove(false);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canAttack = false;
            animator.SetTrigger("Attack");
            StartCoroutine(AllowAttack());
        }
    }
    void StopAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            PlayerMovement.instance.ToggleMove(true);
            animator.SetTrigger("StopAnimation");
        }
    }
    public void ReceiveAttack(int damage, Vector2 knockback, float duration)
    {
        PlayerHealth.instance.Damage(damage);
        PlayerMovement.instance.Knockback(knockback, duration);
    }
    IEnumerator AllowAttack()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }
}