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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là thành và khác phe
        if (collision.gameObject.CompareTag("EnemyCastle"))
        {
            var castle = collision.gameObject.GetComponent<IDamageable>();
            if (castle != null)
            {
                castle.TakeDamage(archerStats.damage); // hoặc archerStats.damage nếu là Archer
            }
        }
    }

}
