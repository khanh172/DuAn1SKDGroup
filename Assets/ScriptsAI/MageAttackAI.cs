using UnityEngine;

public class MageAttackAI : MonoBehaviour
{
    public MageStats stats; // Tham chiếu đến ScriptableObject MageStats
    public GameObject projectilePrefab; // Prefab của quả cầu

    private Transform target;
    private float nextAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator component
    }

    void Update()
    {
        FindTarget();
        MoveAndAttack();
    }

    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, stats.attackRange);
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

    void MoveAndAttack()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget > stats.attackRange)
            {
                // Di chuyển về phía mục tiêu
                MoveTowards(target.position);
            }
            else
            {
                // Tấn công mục tiêu nếu trong tầm đánh
                StopMoving();
                if (Time.time >= nextAttackTime)
                {
                    ShootProjectile(target);
                    nextAttackTime = Time.time + 1f / stats.attackSpeed;
                }
            }
        }
        else
        {
            // Nếu không có mục tiêu, tiếp tục di chuyển về bên phải
            MoveRight();
        }
    }

    void MoveRight()
    {
        transform.Translate(Vector2.right * stats.moveSpeed * Time.deltaTime);
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

    void ShootProjectile(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        BouncingProjectile bouncingProjectile = projectile.GetComponent<BouncingProjectile>();
        if (bouncingProjectile != null)
        {
            // Truyền sát thương từ Mage Stats sang quả cầu
            bouncingProjectile.Initialize(target, stats.damage);
        }
    }
}
