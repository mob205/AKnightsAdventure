using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkShadow : Enemy
{
    public int attackRadius;

    PlayerMovement player;
    bool isAggro;
    Vector3 defaultPos;
    Animator animator;
    Rigidbody2D rb;

    Vector3 movementDir;
    bool canMove = true;
    bool canAttack = true;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = PlayerMovement.instance;
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckAggro();
        Move();

    }

    void CheckAggro()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            isAggro = true;
        }
        else
        {
            isAggro = false;
        }
    }
    void Move()
    {
        if (!canMove) { rb.velocity = Vector2.zero; return; }
        if (isAggro)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                Attack();
            }
            else
            {
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
                movementDir = (player.transform.position - transform.position).normalized;
                rb.velocity = movementDir * movementSpeed;

                animator.SetBool("IsMoving", true);
            }
        } else if (!isAggro)
        {
            if (defaultPos != transform.position)
            {
                //transform.position = Vector3.MoveTowards(transform.position, defaultPos, movementSpeed * Time.deltaTime);
                movementDir = (defaultPos - transform.position).normalized;
                animator.SetBool("IsMoving", true);
                rb.velocity = movementDir * movementSpeed;
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }

        animator.SetBool("IsAggro", isAggro);
        animator.SetFloat("Horizontal", movementDir.x);
        animator.SetFloat("Vertical", movementDir.y);
    }
    void Attack()
    {
        if (!canAttack) { return; }
        canMove = false;
        animator.SetTrigger("Attack");
        canAttack = false;
        StartCoroutine(AllowAttack());
    }
    IEnumerator AllowAttack()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }
    void StopAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            animator.SetTrigger("StopAnimation");
            canMove = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Debug.Log("Enemy taken damage.");
        }
    }
}
