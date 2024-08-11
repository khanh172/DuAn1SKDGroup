using UnityEngine;
using UnityEngine.UI;

public class AllyCastle : MonoBehaviour, IDamageable
{
    public CastleStats castleStats;
    public Slider healthBarSlider;
    private GameManager gameManager;
    private float currentHealth;
    void Start()
    {
        currentHealth = castleStats.health;
        gameManager = FindObjectOfType<GameManager>();
        healthBarSlider.maxValue = castleStats.health;
        healthBarSlider.value = currentHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBarSlider.value = currentHealth;
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

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

}
