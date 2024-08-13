using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour, IDamageable
{
    public MageStats mageStats; // Tham chiếu đến ScriptableObject MageStats
    public GameObject projectilePrefab; // Prefab của quả cầu
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;

    private Transform target;
    private float nextAttackTime = 0f;

    void Start()
    {
        currentHealth = mageStats.health;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FindTarget();
        MoveAndAttack();
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

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, mageStats.attackRange);
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("EnemyCastle"))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.transform;
                }
            }
        }

        target = closestTarget;
    }

    private void MoveAndAttack()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget > mageStats.attackRange)
            {
                // Di chuyển về phía mục tiêu
                MoveTowards(target.position);
            }
            else
            {
                // Dừng lại và tấn công mục tiêu nếu trong tầm đánh
                StopMoving();
                if (Time.time >= nextAttackTime)
                {
                    animator.SetTrigger("Attack");
                    ShootProjectile(target);
                    nextAttackTime = Time.time + 1f / mageStats.attackSpeed;
                }
            }
        }
        else
        {
            // Nếu không có mục tiêu, tiếp tục di chuyển về bên phải
            MoveRight();
        }
    }

    private void MoveRight()
    {
        transform.Translate(Vector2.right * mageStats.moveSpeed * Time.deltaTime);
        animator.SetFloat("Move", mageStats.moveSpeed);
    }

    private void MoveTowards(Vector2 position)
    {
        Vector2 direction = (position - (Vector2)transform.position).normalized;
        transform.Translate(direction * mageStats.moveSpeed * Time.deltaTime);
        animator.SetFloat("Move", mageStats.moveSpeed);
    }

    private void StopMoving()
    {
        animator.SetFloat("Move", 0);
    }

    private void ShootProjectile(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        BouncingProjectile bouncingProjectile = projectile.GetComponent<BouncingProjectile>();
        if (bouncingProjectile != null)
        {
            bouncingProjectile.Initialize(target, mageStats.damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyCastle"))
        {
            var castle = collision.gameObject.GetComponent<IDamageable>();
            if (castle != null)
            {
                castle.TakeDamage(mageStats.damage);
            }
        }
    }
}
