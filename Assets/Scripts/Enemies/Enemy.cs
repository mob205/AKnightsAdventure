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
    public float attackCD;

    [Header("AI")]
    public int aggroRange;
    public int attackRange;



    void Start()
    {
    }

    void Update()
    {
        
    }
}
