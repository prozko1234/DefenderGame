using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public void SetHp(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
        Debug.Log("Current Hp setted to: " + currentHealth + "Max Hp setted to: " + maxHealth);
    }
    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetAddCurrentHp(float hp)
    {
        this.currentHealth += hp;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Made " + damage + " damage." + "\n Health left: " + currentHealth);
    }
}
