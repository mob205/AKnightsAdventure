using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float playerSpeed = 5;

    Animator animator;
    Rigidbody2D rb;
    bool canMove = true;
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
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        //transform.position = transform.position + movement * playerSpeed * Time.deltaTime;
        rb.velocity = new Vector2(movement.x * playerSpeed, movement.y * playerSpeed);

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
        if (Input.GetMouseButton(0))
        {
            canMove = false;
            animator.SetTrigger("Attack");
        }
    }
    void StopAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackBackward"))
        {
            canMove = true;
            animator.SetTrigger("StopAnimation");
        }
    }
}
