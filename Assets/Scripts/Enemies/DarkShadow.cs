using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkShadow : Enemy
{

    PlayerMovement player;
    bool isAggro;
    Vector3 defaultPos;
    Animator animator;
    Rigidbody2D rb;

    public Vector3 movementDir;
    bool canMove = true;
    bool canAttack = true;

    bool isKnockbacked;
    float knockbackDuration;

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
    void ToggleMove(bool moveability)
    {
        if (isKnockbacked) { return; }
        canMove = moveability;
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
        if (!canMove) { return; }
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
            if (Vector3.Distance(transform.position, defaultPos) > 1f)
            {
                //transform.position = Vector3.MoveTowards(transform.position, defaultPos, movementSpeed * Time.deltaTime);
                movementDir = (defaultPos - transform.position).normalized;
                animator.SetBool("IsMoving", true);
                rb.velocity = movementDir * movementSpeed;
            }
            else
            {
                animator.SetBool("IsMoving", false);
                rb.velocity = Vector3.zero;
            }
        }

        animator.SetBool("IsAggro", isAggro);
        animator.SetFloat("Horizontal", movementDir.x);
        animator.SetFloat("Vertical", movementDir.y);
    }
    void Attack()
    {
        if (!canAttack) { return; }
        if (isKnockbacked) { return; }
        canMove = false;
        rb.velocity = Vector2.zero;
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
    void Knockback(Vector2 knockback, float duration)
    {
        if (isKnockbacked) { Debug.Log("is already knockbacked"); return; }

        knockbackDuration = duration;
        StartCoroutine("ApplyKnockback", knockback);
    }
    IEnumerator ApplyKnockback(Vector2 knockback)
    {
        ToggleMove(false);
        isKnockbacked = true;

        rb.velocity = Vector2.zero;
        rb.AddForce(knockback, ForceMode2D.Impulse);

        animator.SetBool("IsDamaged", true);

        yield return new WaitForSeconds(knockbackDuration);

        animator.SetBool("IsDamaged", false);

        isKnockbacked = false;
        ToggleMove(true);
    }
    void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Enemy has died");
            //initiate death sequence.
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Damage(PlayerCombat.instance.damage);
            Knockback((transform.position - player.transform.position).normalized * PlayerCombat.instance.knockback, 1f);
        }
    }
}
