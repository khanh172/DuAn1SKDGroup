using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour, IDamageable
{
    public ArcherStats archerStats;
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = archerStats.health;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                castle.TakeDamage(archerStats.damage); // hoặc archerStats.damage nếu là Archer
            }
        }
    }

}
