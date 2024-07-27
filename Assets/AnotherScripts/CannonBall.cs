using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 50f;

    private Vector2 targetPosition;

    public void Launch(Vector2 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        // Di chuyển đạn pháo về phía mục tiêu
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Nếu đến vị trí mục tiêu thì phá hủy đạn pháo
        if ((Vector2)transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Gây sát thương cho đơn vị Enemy
            IDamageable enemy = collision.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
