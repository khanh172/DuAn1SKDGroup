using System.Collections.Generic;
using UnityEngine;

public class BouncingProjectile : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển của quả cầu
    public int maxBounces = 2; // Số lần nảy tối đa
    private int currentBounces = 0;
    private float damage;
    private Transform target;
    private List<Transform> hitTargets = new List<Transform>();

    public void Initialize(Transform initialTarget, float damage)
    {
        this.damage = damage;
        target = initialTarget;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Hủy nếu không có mục tiêu
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        var damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        hitTargets.Add(target);

        if (currentBounces < maxBounces)
        {
            FindNextTarget();
            currentBounces++;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FindNextTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider2D hit in hits)
        {
            if ((hit.CompareTag("Enemy") || hit.CompareTag("EnemyCastle")) && !hitTargets.Contains(hit.transform))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget;
            MoveTowardsTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
