using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public string enemyName;
    public float movementSpeed;
    public int attack;
    public int knockback;
    public float kbDuration;
    public float attackCD;

    [Header("AI")]
    public int aggroRange;
    public int attackRange;


    protected bool isAggro;
    protected bool canMove = true;
    protected bool canAttack = true;
    protected bool isKnockbacked;
    protected bool isDead;

    protected PlayerMovement player;
    protected Vector3 defaultPos;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected Vector3 movementDir;
    protected float knockbackDuration;

    protected void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = PlayerMovement.instance;
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        if (PlayerCombat.instance.isDead) { return; }
        CheckAggro();
        Move();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartDeath();
        }
    }
    protected void ToggleMove(bool moveability)
    {
        if (isKnockbacked) { return; }
        canMove = moveability;
    }
    protected void CheckAggro()
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
    protected void Move()
    {
        if (isDead || !canMove) { return; }
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
        }
        else if (!isAggro)
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
    protected void StartDeath()
    {
        rb.velocity = Vector2.zero;
        //animator.SetBool("IsDead", true);
        animator.SetTrigger("OnDeath");
    }
    protected void Attack()
    {
        if (isKnockbacked || !canAttack) { return; }
        canMove = false;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        canAttack = false;
        StartCoroutine(AllowAttack());
    }
    protected IEnumerator AllowAttack()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }
    protected void StopAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            animator.SetTrigger("StopAnimation");
            canMove = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            Debug.Log("Enemy has died.");
            Destroy(gameObject);
        }
    }
    protected void Knockback(Vector2 knockback, float duration)
    {
        if (isKnockbacked) { return; }

        knockbackDuration = duration;
        StartCoroutine("ApplyKnockback", knockback);
    }
    protected IEnumerator ApplyKnockback(Vector2 knockback)
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

        if (isDead)
        {
            isKnockbacked = true;
            StartDeath();
        }
    }

    protected void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            isDead = true;
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Damage(PlayerCombat.instance.damage);
            Knockback((transform.position - player.transform.position).normalized * PlayerCombat.instance.knockback, 1f);
        }
    }


}
