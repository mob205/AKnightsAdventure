using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]
    public static PlayerHealth instance;

    public HeartUI[] hearts;

    public delegate void OnHealDelegate(int health);
    public delegate void OnDamageDelegate(int health);
    public static event OnHealDelegate OnHeal;
    public static event OnDamageDelegate OnDamage;

    public int baseHealth = 40;
    public static int health;
    public static int maxHealth;
    public float deathDelay;

    public bool canDie;

    public RectTransform[] uiElements;
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

    public void IncreaseMaxHP(int amount)
    {
        maxHealth += amount;
        health = maxHealth;
        CalculateHeartUI();
    }
    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0 && canDie)
        {
            
            PlayerCombat.instance.isDead = true;
            StartDeathSequence();
        }
        if(OnDamage != null) { OnDamage(health); }
        CalculateHeartUI();
    }
    public void StartDeathSequence()
    {
        StartCoroutine(DeathSequence());
    }
    public IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathDelay);
        foreach(var element in uiElements)
        {
            element.gameObject.SetActive(false);
        }
        FadeToBlack.instance.Fade(5f, 3f);
        Messenger.instance.Message("You Died", Color.red, 3f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level 1");
    }
    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        if (OnHeal != null) { OnHeal(health); }
        CalculateHeartUI();
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
