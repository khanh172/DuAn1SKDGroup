using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
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
    private float archerCooldown = 3.0f;   // Cooldown cho Archer
    private float lastInfantrySpawnTime;
    private float lastArcherSpawnTime;

    private void Start()
    {
        Time.timeScale = 1.0f;
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

            if (CanSpawnUnitWithCooldown() /*&& IsPositionInSpawnArea(spawnPosition)*/)
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
            return 15;
        }
        else if (unitToSpawn == archerPrefab)
        {
            return 20;
        }
        return 0;
    }

    private Vector2 GetSpawnPosition()
    {
        //Vector3 position3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 position2D = new Vector2(position3D.x, position3D.y);
        //return position2D;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        float clampedX = Mathf.Clamp(worldPos.x, unitSpawner.spawnArea.xMin, unitSpawner.spawnArea.xMax);
        float clampedY = Mathf.Clamp(worldPos.y, unitSpawner.spawnArea.yMin, unitSpawner.spawnArea.yMax);

        return new Vector2(clampedX, clampedY);




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
    public void NextLevel()
    {
        // Lấy tên của Scene hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Kiểm tra và chuyển đến level tiếp theo
        if (currentSceneName == "Level1")
        {
            SceneManager.LoadScene("Level2");   
        }
        else if (currentSceneName == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        // Nếu bạn có thêm các level khác, bạn có thể tiếp tục thêm điều kiện vào đây
    }
    public void GoToMainMenu()
    {
        // Đặt Time.timeScale về 1 để đảm bảo Menu chính hoạt động bình thường
        Time.timeScale = 1.0f;

        // Chuyển sang Scene Menu
        SceneManager.LoadScene("Menu");
    }
    public void PlayAgain()
    {
        // Đặt Time.timeScale về 1 để đảm bảo game chạy bình thường khi chơi lại
        Time.timeScale = 1.0f;

        // Lấy tên của Scene hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Tải lại Scene hiện tại để chơi lại
        SceneManager.LoadScene(currentSceneName);
    }

    private IEnumerator IncrementGold()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            gold += 5;
        }
    }
}
