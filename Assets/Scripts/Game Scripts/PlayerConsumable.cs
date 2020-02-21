using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerConsumable : MonoBehaviour
{
    public int healthPotionStrength = 4;
    public int baseHealthPotionAmount = 3;
    public float potionCooldown = 2f;

    int healthPotionAmount;
    TextMeshProUGUI potionCounter;
    bool canUsePotion = true;

    private void Start()
    {
        healthPotionAmount = baseHealthPotionAmount;
        potionCounter = GameObject.FindGameObjectWithTag("HealthPotionCounter").GetComponent<TextMeshProUGUI>();
        UpdateCounter();
    }
    private void Update()
    {
        if(Input.GetAxisRaw("UsePotion") == 1 && healthPotionAmount > 0 && canUsePotion && PlayerHealth.health != PlayerHealth.maxHealth)
        {
            UsePotion();
        }
    }
    public void GivePotion(int amount)
    {
        healthPotionAmount += amount;
        UpdateCounter();
    }
    void UsePotion()
    {
        PlayerHealth.instance.Heal(healthPotionStrength);
        healthPotionAmount--;
        UpdateCounter();
        StartCoroutine(CooldownPotion());
    }
    void UpdateCounter()
    {
        potionCounter.text = "x" + healthPotionAmount.ToString();
    }
    IEnumerator CooldownPotion()
    {
        canUsePotion = false;
        yield return new WaitForSeconds(potionCooldown);
        canUsePotion = true;
    }
}
