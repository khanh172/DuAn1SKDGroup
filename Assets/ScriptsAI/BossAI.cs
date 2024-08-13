using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour, IDamageable
{
    public BossStats bossStats;
    private Animator animator;
    private Transform target;
    private float currentHealth;
    private bool isMoving = false;
    private bool isDead = false;
    private float nextAttackTime = 0f;
   

    public Slider healthSlider; // Thêm thanh máu

    private float attackDuration = 1f; // Điều chỉnh giá trị này cho phù hợp với độ dài animation
    private float currentAttackTime = 0f;

    private bool isMovingAnimationComplete = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = bossStats.health;
        StartCoroutine(MoveRoutine());

        healthSlider.maxValue = bossStats.health;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        if (isDead) return;
        
        FindTarget();
        AttackTarget();
    }

    private void FindTarget()
    {
        if (target == null)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, bossStats.attackRange);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Ally") || hit.CompareTag("AllyCastle"))
                {
                    float distance = Vector2.Distance(transform.position, hit.transform.position);
                    if (distance <= bossStats.attackRange)
                    {
                        target = hit.transform;
                        break;
                    }
                }
            }
        }
    }

    private void AttackTarget()
    {
        if (isMoving || !isMovingAnimationComplete)
        {
            // Nếu đang di chuyển, đảm bảo không ở trạng thái tấn công
            animator.SetBool("isAttacking", false);
            return;
        }

        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget <= bossStats.attackRange)
            {
                if (Time.time >= nextAttackTime)
                {
                    StartAttack();
                }
                else if (animator.GetBool("isAttacking"))
                {
                    currentAttackTime += Time.deltaTime;
                    if (currentAttackTime >= attackDuration)
                    {
                        EndAttack();
                    }
                }
            }
            else
            {
                EndAttack();
            }
        }
        else
        {
            EndAttack();
        }
    }


    private void StartAttack()
    {
        animator.SetBool("isAttacking", true);
        currentAttackTime = 0f;
        var damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(bossStats.damage);
        }
        nextAttackTime = Time.time + 1f / bossStats.attackSpeed;
    }

    private void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        currentAttackTime = 0f;
    }
    private IEnumerator MoveRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(10f); // Đợi 10 giây
            StartMoving();
            yield return new WaitForSeconds(2f); // Di chuyển trong 2 giây
            StopMoving();
        }
    }

    private void StartMoving()
    {
        isMoving = true;
        isMovingAnimationComplete = false;
        animator.SetBool("isMoving", true);
        StartCoroutine(MoveTowardsAllyCastle());
    }
    public void OnMoveAnimationComplete()
    {
        isMovingAnimationComplete = true;
    }

    private void StopMoving()
    {
        isMoving = false;
        animator.SetBool("isMoving", false);
        // Đặt một độ trễ nhỏ trước khi cho phép tấn công
        StartCoroutine(DelayBeforeAttack());
    }

    private IEnumerator DelayBeforeAttack()
    {
        yield return new WaitForSeconds(0.5f); // Điều chỉnh thời gian nếu cần
        isMovingAnimationComplete = true;
    }

    private IEnumerator MoveTowardsAllyCastle()
    {
        while (isMoving && !isDead)
        {
            Vector2 movement = Vector2.left * bossStats.moveSpeed * Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, movement.magnitude, LayerMask.GetMask("Ally"));

            if (hit.collider != null)
            {
                // Nếu phát hiện Ally, dừng di chuyển
                StopMoving();
                yield break;
            }

            transform.Translate(movement);
            yield return null;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetBool("isDead", true);
        StopAllCoroutines();
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        // Đợi cho animation death kết thúc
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Sau khi animation kết thúc, vô hiệu hóa các component không cần thiết
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // Thêm fade effect nếu cần
        // StartCoroutine(FadeOutEffect());

        // Đợi thêm một khoảng thời gian ngắn trước khi destroy
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
   
}
