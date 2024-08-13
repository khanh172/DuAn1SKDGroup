using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IDamageable
{
    public UnitEnemyStats skeletonStats;
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = skeletonStats.health;
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

    private IEnumerator DestroyAfterAnimation()
    {
        // Đợi cho animation Die hoàn tất trước khi hủy gameObject
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllyCastle"))
        {
            var castle = collision.gameObject.GetComponent<IDamageable>();
            if (castle != null)
            {
                castle.TakeDamage(skeletonStats.damage);
            }
        }
    }
}

