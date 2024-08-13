﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyCastle : MonoBehaviour, IDamageable
{
    public EnemyCastleStats castleStats;
    public GameObject[] enemyPrefabs;
    public float spawnIntervalMin = 1f;
    public float spawnIntervalMax = 5f;
    public Slider hbSlider;

    public GameObject[] enemySecondPrefabs;
    public float spawnIntervalSecondMin = 1f;
    public float spawnIntervalSecondMax = 5f;

    private GameManager gameManager;
    private float currentHealth;

    void Start()
    {
        currentHealth = castleStats.health;
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnEnemiesSecond());

        hbSlider.maxValue = castleStats.health;
        hbSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {       
        currentHealth -= damage;
        hbSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameManager.GameOver(true);
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Chọn ngẫu nhiên khoảng thời gian giữa các lần triệu hồi
            float spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);

            // Chọn ngẫu nhiên vị trí trong khoảng X = 5 tới 7 và Y = -4 tới 2
            float spawnX = Random.Range(5f, 7f);
            float spawnY = Random.Range(-4f, 2f);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            // Chọn ngẫu nhiên một quân lính từ prefab
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnEnemiesSecond()
    {

        while (true)
        {
            // Chọn ngẫu nhiên khoảng thời gian giữa các lần triệu hồi
            float spawnSecondInterval = Random.Range(spawnIntervalSecondMin, spawnIntervalSecondMax);
            yield return new WaitForSeconds(spawnSecondInterval);

            // Chọn ngẫu nhiên vị trí trong khoảng X = 5 tới 7 và Y = -4 tới 2
            float spawnSecondX = Random.Range(5f, 7f);
            float spawnSecondY = Random.Range(-4f, 2f);
            Vector2 spawnSecondPosition = new Vector2(spawnSecondX, spawnSecondY);

            // Chọn ngẫu nhiên một quân lính từ prefab
            int randomSecondIndex = Random.Range(0, enemySecondPrefabs.Length);
            GameObject enemySecond = Instantiate(enemySecondPrefabs[randomSecondIndex], spawnSecondPosition, Quaternion.identity);
        }
    }

}
