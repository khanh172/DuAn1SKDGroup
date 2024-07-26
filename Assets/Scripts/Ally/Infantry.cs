using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : MonoBehaviour, IDamageable
{
    public InfantryStats infantryStats;
    private float currentHealth;

    void Start()
    {
        currentHealth = infantryStats.health;
    }

    void Update()
    {
        // Update logic (if any) can go here
    }

    // Method to handle receiving damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle the infantry's death
    public void Die()
    {
        // Add logic for what happens when the infantry dies
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
