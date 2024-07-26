using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    public SoldierStats stats; // Sử dụng SoldierStats để quản lý các thông số của kẻ thù
    public float searchRange; // Khoảng cách để tìm kiếm kẻ thù

    private Transform target;
    private float nextAttackTime = 0f;

    void Update()
    {
        FindTarget(); // Tìm kiếm mục tiêu trước
        MoveAndAttack(); // Di chuyển và tấn công
    }

    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRange);
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ally")) // Tìm các đồng minh (đối tượng có tag "Ally")
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

        if (target == null)
        {
            Debug.LogWarning("No target found within range.");
        }
    }

    void MoveAndAttack()
    {
        if (stats == null)
        {
            Debug.LogError("SoldierStats is not assigned!");
            return;
        }

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
                // Dừng lại và tấn công mục tiêu nếu trong tầm đánh
                StopMoving();
                if (Time.time >= nextAttackTime)
                {
                    // Tấn công mục tiêu với sát thương từ SoldierStats
                    var damageable = target.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.TakeDamage(stats.damage);
                    }
                    else
                    {
                        Debug.LogWarning("Target does not implement IDamageable.");
                    }
                    nextAttackTime = Time.time + 1f / stats.attackSpeed;
                }
            }
        }
        else
        {
            // Nếu không có mục tiêu, tiếp tục di chuyển về bên trái
            MoveLeft();
        }
    }


    void MoveLeft()
    {
        // Di chuyển về bên trái với tốc độ từ SoldierStats
        transform.Translate(Vector2.left * stats.moveSpeed * Time.deltaTime);
    }

    void MoveTowards(Vector2 position)
    {
        Vector2 direction = (position - (Vector2)transform.position).normalized;
        transform.Translate(direction * stats.moveSpeed * Time.deltaTime);
    }

    void StopMoving()
    {
        // Giữ nguyên vị trí, ngăn chặn việc di chuyển
        transform.Translate(Vector2.zero);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ngăn chặn việc đẩy các GameObject khác khi va chạm
        if (collision.gameObject.CompareTag("Ally"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                Debug.LogWarning("Rigidbody2D component not found on the object.");
            }
        }
    }
}
