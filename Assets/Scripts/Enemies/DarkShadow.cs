using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkShadow : Enemy
{
    PlayerMovement player;
    bool isAggro;
    Vector3 defaultPos;

    void Start()
    {
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
        if (!isAggro)
        {
            Vector3.MoveTowards(transform.position, defaultPos, movementSpeed * Time.deltaTime);
            return;
        }
        if(Vector3.Distance(player.transform.position, transform.position) <= attackRadius)
        {
            Attack();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
    }
    void Attack()
    {

    }

}
