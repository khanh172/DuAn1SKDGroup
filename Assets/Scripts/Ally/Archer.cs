using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour, IDamageable
{
    public ArcherStats archerStats;
    private float currentHealth;

    void Start()
    {
        currentHealth = archerStats.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
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
