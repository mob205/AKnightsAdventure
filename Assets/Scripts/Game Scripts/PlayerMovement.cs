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

    public bool hasKey;
    private bool canMove = true;

    [HideInInspector]
    public bool isKnockbacked;
    
    private float kbDuration;


    private void Awake()
    {
        instance = this;
    }

    void Start () {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate ()
    {
        ProcessMovement();
    }

    public void ToggleMove(bool moveability)
    {
        if (isKnockbacked) { return; }
        canMove = moveability;
    }

    private void ProcessMovement()
    {
        if(Time.timeScale == 0) { return; }
        if (!canMove) { return; }
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;
        //rb.velocity = new Vector2(movement.x * playerSpeed, movement.y * playerSpeed
        rb.MovePosition(transform.position + movement * playerSpeed * Time.fixedDeltaTime);

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
    public void Knockback(Vector2 knockback, float duration)
    {
        kbDuration = duration;
        StartCoroutine("ApplyKnockback", knockback);
        
    }
    IEnumerator ApplyKnockback(Vector2 knockback)
    {
        ToggleMove(false);
        isKnockbacked = true;

        rb.velocity = Vector2.zero;
        rb.AddForce(knockback, ForceMode2D.Impulse);

        animator.SetBool("IsMoving", false);

        yield return new WaitForSeconds(kbDuration);

        isKnockbacked = false;
        ToggleMove(true);

        if (PlayerCombat.instance.isDead)
        {
            rb.velocity = Vector2.zero;
            PlayerCombat.instance.StartDeath();
        }
    }
    public void BuffMovement(float amount)
    {
        playerSpeed += amount;
    }
    
    public void SetKey(bool value)
    {
        hasKey = value;
    }
}
