using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float playerSpeed = 5;
    [SerializeField] float attackCD = 0.5f;

    private Animator animator;
    private Rigidbody2D rb;
    private bool canMove = true;
    private bool canAttack = true;
	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessMovement();
        ProcessAttacks();
    }

    private void ProcessMovement()
    {
        if (!canMove) { rb.velocity = Vector2.zero; return; }
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;
        rb.velocity = new Vector2(movement.x * playerSpeed, movement.y * playerSpeed);
        Debug.Log(rb.velocity.magnitude);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        if(Math.Abs(movement.magnitude) > 0)
        {
            animator.SetBool("IsMoving", true);

            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
    private void ProcessAttacks()
    {
        if(!canAttack) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            canMove = false;
            canAttack = false;
            animator.SetTrigger("Attack");
            StartCoroutine(AllowAttack());
        }
    }
    void StopAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            canMove = true;
            animator.SetTrigger("StopAnimation");
        }
    }
    IEnumerator AllowAttack()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }
}
