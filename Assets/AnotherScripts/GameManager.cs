using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject infantryPrefab;
    public GameObject archerPrefab;
    public UnitSpawner unitSpawner;
    private GameObject unitToSpawn;
    public Text goldText;

    public GameObject panelWin;
    public GameObject panelLose;

    private bool isUnitSelected = false;
    private Vector2 spawnPosition;

    private int gold = 0;

    private float infantryCooldown = 2.0f; // Cooldown cho Infantry
    private float archerCooldown = 4.0f;   // Cooldown cho Archer
    private float lastInfantrySpawnTime;
    private float lastArcherSpawnTime;

    private void Start()
    {
        StartCoroutine(IncrementGold());
        panelWin.SetActive(false);
        panelLose.SetActive(false);
    }

    void Update()
    {
        goldText.text = "Vàng: " + gold.ToString();

        if (isUnitSelected && Input.GetMouseButtonDown(0))
        {
            spawnPosition = GetSpawnPosition();

            if (CanSpawnUnitWithCooldown() && IsPositionInSpawnArea(spawnPosition))
            {
                unitSpawner.SpawnUnit(unitToSpawn, spawnPosition);
                gold -= GetUnitCost();
                isUnitSelected = false;
                unitToSpawn = null;
            }
            else
            {
                Debug.Log("Không đủ vàng hoặc không thể triệu hồi lính do thời gian hồi!");
            }
        }
    }

    public void SelectInfantry()
    {
        unitToSpawn = infantryPrefab;
        isUnitSelected = true;
    }

    public void SelectArcher()
    {
        unitToSpawn = archerPrefab;
        isUnitSelected = true;
    }

    private bool CanSpawnUnit()
    {
        return gold >= GetUnitCost();
    }

    private bool CanSpawnUnitWithCooldown()
    {
        if (unitToSpawn == infantryPrefab)
        {
            if (Time.time >= lastInfantrySpawnTime + infantryCooldown && CanSpawnUnit())
            {
                lastInfantrySpawnTime = Time.time;
                return true;
            }
        }
        else if (unitToSpawn == archerPrefab)
        {
            if (Time.time >= lastArcherSpawnTime + archerCooldown && CanSpawnUnit())
            {
                lastArcherSpawnTime = Time.time;
                return true;
            }
        }
        return false;
    }

    private int GetUnitCost()
    {
        if (unitToSpawn == infantryPrefab)
        {
            return 10;
        }
        else if (unitToSpawn == archerPrefab)
        {
            return 20;
        }
        return 0;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector3 position3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position2D = new Vector2(position3D.x, position3D.y);
        return position2D;
    }

    private bool IsPositionInSpawnArea(Vector2 position)
    {
        return unitSpawner.IsPositionInSpawnArea(position);
    }

    public void GameOver(bool isAllyWin)
    {
        if (isAllyWin)
        {
            panelWin.SetActive(true);
        }
        else
        {
            panelLose.SetActive(true);
        }

        Time.timeScale = 0;
    }

    private IEnumerator IncrementGold()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            gold += 200;
        }
    }
}
