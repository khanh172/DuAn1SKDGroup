using UnityEngine;

public class AllyCastle : MonoBehaviour, IDamageable
{
    public CastleStats castleStats;
    public GameObject cannonBallPrefab;
    public float cannonRange;
    private GameManager gameManager;

    private float currentHealth;
    private bool isCannonMode = false;

    void Start()
    {
        currentHealth = castleStats.health;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (isCannonMode && Input.GetMouseButtonDown(0))
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(transform.position, targetPosition) <= cannonRange)
            {
                FireCannon(targetPosition);
                isCannonMode = false; // Reset l?i sau khi b?n
            }
        }
    }

    public void ActivateCannonMode()
    {
        isCannonMode = true;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameManager.GameOver(false);
        Destroy(gameObject);
    }

    private void FireCannon(Vector2 targetPosition)
    {
        GameObject cannonBall = Instantiate(cannonBallPrefab, transform.position, Quaternion.identity);
        cannonBall.GetComponent<CannonBall>().Launch(targetPosition);
    }
}