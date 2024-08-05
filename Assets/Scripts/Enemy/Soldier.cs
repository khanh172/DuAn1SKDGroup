using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour, IDamageable
{
    public SoldierStats soldierStats;
    private float currentHealth;

    void Start()
    {
        currentHealth = soldierStats.health;
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

    // Method to handle the soldier's death
    public void Die()
    {
        // Add logic for what happens when the soldier dies
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là thành và khác phe
        if (collision.gameObject.CompareTag("AllyCastle"))
        {
            var castle = collision.gameObject.GetComponent<IDamageable>();
            if (castle != null)
            {
                castle.TakeDamage(soldierStats.damage);
            }
        }
    }

}
