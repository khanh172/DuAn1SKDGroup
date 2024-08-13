using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    public UnitEnemyStats stats;
    public float searchRange;
    private Transform target;
    private float nextAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FindTarget();
        MoveAndAttack();
    }

    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRange);
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ally") || hit.CompareTag("AllyCastle"))
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

    void MoveAndAttack()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget > stats.attackRange)
            {
                MoveTowards(target.position);
            }
            else
            {
                StopMoving();
                if (Time.time >= nextAttackTime)
                {
                    animator.SetTrigger("Attack");
                    var damageable = target.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.TakeDamage(stats.damage);
                    }
                    nextAttackTime = Time.time + 1f / stats.attackSpeed;
                }
            }
        }
        else
        {
            MoveLeft();
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * stats.moveSpeed * Time.deltaTime);
        animator.SetFloat("Move", stats.moveSpeed);
    }

    void MoveTowards(Vector2 position)
    {
        Vector2 direction = (position - (Vector2)transform.position).normalized;
        transform.Translate(direction * stats.moveSpeed * Time.deltaTime);
        animator.SetFloat("Move", stats.moveSpeed);
    }

    void StopMoving()
    {
        animator.SetFloat("Move", 0);
    }
}
