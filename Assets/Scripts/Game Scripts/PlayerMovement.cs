using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float playerSpeed = 5;

    [HideInInspector]
    public static PlayerMovement instance;

    private Animator animator;
    private Rigidbody2D rb;
    private bool canMove = true;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

	void Update ()
    {
        ProcessMovement();
    }

    public void ToggleMove(bool moveability)
    {
        canMove = moveability;
    }

    private void ProcessMovement()
    {
        if(Time.timeScale == 0) { return; }
        if (!canMove) { rb.velocity = Vector2.zero; return; }
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;
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
}
