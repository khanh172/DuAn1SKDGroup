using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherGoblin : MonoBehaviour, IDamageable
{
    public ArcherGoblinStats archerGoblinStats;
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = archerGoblinStats.health;
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
    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        StartCoroutine(DestroyAfterAnimation());
    }
    // Method to handle the soldier's death
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
        if (collision.gameObject.CompareTag("AllyCastle"))
        {
            var castle = collision.gameObject.GetComponent<IDamageable>();
            if (castle != null)
            {
                castle.TakeDamage(archerGoblinStats.damage);
            }
        }
    }

}
