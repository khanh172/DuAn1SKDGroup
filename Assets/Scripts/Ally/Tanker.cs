using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : MonoBehaviour, IDamageable
{
    public TankerStats infantryStats;
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = infantryStats.health;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update logic (if any) can go here
    }

    // Method to handle receiving damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    // Method to handle the infantry's death
    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        StartCoroutine(DestroyAfterAnimation());
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
    public bool IsDead()
    {
        return isDead;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là thành và khác phe
        if (collision.gameObject.CompareTag("EnemyCastle"))
        {
            var castle = collision.gameObject.GetComponent<IDamageable>();
            if (castle != null)
            {
                castle.TakeDamage(infantryStats.damage); // hoặc archerStats.damage nếu là Archer
            }
        }
    }

}
