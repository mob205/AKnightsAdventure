using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]
    public static PlayerHealth instance;

    public HeartUI[] hearts;

    public delegate void OnHealDelegate(int health);
    public delegate void OnDamageDelegate(int health);
    public static event OnHealDelegate OnHeal;
    public static event OnDamageDelegate OnDamage;

    public static int baseHealth = 40;
    static int health;
    static int maxHealth;

    private void Awake()
    {
        instance = this;        
    }

    void Start()
    {
        maxHealth = baseHealth;
        health = maxHealth;
        CalculateHeartUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Damage(10);
        }
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            //initiate death sequence.
        }
        if(OnDamage != null) { OnDamage(health); }
    }
    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        if (OnHeal != null) { OnHeal(health); }
    }

    void CalculateHeartUI()
    {
        var maxHearts = Math.Ceiling((decimal)maxHealth / 4);
        int remainder;
        var fullHearts = Math.DivRem(health, 4, out remainder);
        for(int i = 0; i < hearts.Length; i++)
        {
            if (fullHearts > 0)
            {
                hearts[i].SetSprite(4);
                fullHearts--;
            }
            else if (remainder > 0)
            {
                hearts[i].SetSprite(remainder);
                remainder = 0;
            }else if(i < maxHearts)
            {
                hearts[i].SetSprite(0);
            }
            else
            {
                hearts[i].SetSpriteNull();
            }
        }
    }

}
