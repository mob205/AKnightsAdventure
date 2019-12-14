using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{

    public Enemy parent;
    

    bool canDamage;
    private void Start()
    {

    }
    private void OnEnable()
    {
        canDamage = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var knockback = (PlayerCombat.instance.transform.position - transform.position).normalized * parent.knockback;
            PlayerCombat.instance.ReceiveAttack(parent.attack, knockback, parent.kbDuration);
            canDamage = false;
        }
    }
}
