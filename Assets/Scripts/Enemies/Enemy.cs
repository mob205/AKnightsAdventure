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

    [Header("AI")]
    public int aggroRange;
    public int attackRange;
    public int attackRadius;



    void Start()
    {
    }

    void Update()
    {
        
    }
}
