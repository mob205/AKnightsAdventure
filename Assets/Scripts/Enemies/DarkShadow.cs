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

    Vector3 movementDir;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = PlayerMovement.instance;
        defaultPos = transform.position;
    }

    void Update()
    {
        CheckAggro();
        Move();
        Debug.Log(defaultPos);
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
        if (isAggro)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                Attack();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
                movementDir = (player.transform.position - transform.position).normalized;

                animator.SetBool("IsMoving", true);
            }
        } else if (!isAggro)
        {
            if(defaultPos != transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, defaultPos, movementSpeed * Time.deltaTime);
                movementDir = (defaultPos - transform.position).normalized;
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
        animator.SetBool("IsAggro", isAggro);
        animator.SetFloat("Horizontal", movementDir.x);
        animator.SetFloat("Vertical", movementDir.y);



    //    if (!isAggro && !(defaultPos == transform.position))
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, defaultPos, movementSpeed * Time.deltaTime);
    //        movementDir = (defaultPos - transform.position).normalized;

    //        animator.SetBool("IsMoving", true);
    //        animator.SetBool("IsAggro", false);
    //        animator.SetFloat("Horizontal", movementDir.x);
    //        animator.SetFloat("Vertical", movementDir.y);
    //        return;
    //    }
    //    else if (!isAggro)
    //    {
    //        animator.SetBool("IsMoving", false);
    //        animator.SetBool("IsAggro", false);
    //        return;
    //    }
    //    if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
    //    {
    //        Attack();
    //    }
    //    else
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    //        movementDir = (player.transform.position - transform.position).normalized;

    //        animator.SetFloat("Horizontal", movementDir.x);
    //        animator.SetFloat("Vertical", movementDir.y);
    //        animator.SetBool("IsMoving", true);
    //        animator.SetBool("IsAggro", true);
    //    }
    }
    void Attack()
    {
        Debug.Log(string.Format("Damaging player by {0}", attack));
        //PlayerHealth.instance.Damage(attack);
    }

}
