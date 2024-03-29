﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float attackCD = 0.5f;
    [SerializeField] AudioSource audioSource;

    public float knockback = 10f;
    public int damage = 5;

    [HideInInspector]
    public static PlayerCombat instance;

    private SpriteRenderer sprite;
    private Animator animator;
    private bool canAttack = true;

    [HideInInspector]
    public bool isDead;

    void Start()
    {
        instance = this;
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
            audioSource.Play();
        }
    }
    void StopAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            PlayerMovement.instance.ToggleMove(true);
            animator.SetTrigger("StopAnimation");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            sprite.enabled = false;
            Debug.Log("Player has died");
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
    public void StartDeath()
    {
        animator.SetTrigger("OnDeath");
        canAttack = false;
        PlayerMovement.instance.ToggleMove(false);
    }
    public void BuffDamage(int amount)
    {
        damage += amount;
    }
}
