 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public Enemy parent;
    
    [HideInInspector]
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var knockback = (PlayerCombat.instance.transform.position - transform.position).normalized * parent.knockback;
            PlayerCombat.instance.ReceiveAttack(parent.attack, knockback, parent.kbDuration);
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("PlayerAttack"))
        {
            gameObject.SetActive(false);
        }
    }
}
