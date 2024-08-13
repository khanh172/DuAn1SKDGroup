using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject infantryPrefab;
    public GameObject archerPrefab;
    public GameObject magePrefab;
    public GameObject tankerPrefab;
    public UnitSpawner unitSpawner;
    private GameObject unitToSpawn;
    public Text goldText;

    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject settingPanel;

    public Button infantryButton;
    public Button archerButton;
    public Button mageButton;
    public Button tankerButton;
    
    public Button doneButton;
    public Button quitButton;


    public Image infantryImage;
    public Image archerImage;
    public Image mageImage;
    public Image tankerImage;


    private bool isUnitSelected = false;
    private Vector2 spawnPosition;

    private int gold = 0;

    private float infantryCooldown = 4.0f; // Cooldown cho Infantry
    private float archerCooldown = 6.0f;   // Cooldown cho Archer
    private float mageCooldown = 8.0f;
    private float tankerCooldown = 9.0f;
    private float lastInfantrySpawnTime;
    private float lastArcherSpawnTime;
    private float lastMageSpawnTime;
    private float lastTankerSpawnTime;

    private void Start()
    {
        infantryImage = infantryButton.GetComponent<Image>();
        archerImage = archerButton.GetComponent<Image>();
        mageImage = mageButton.GetComponent<Image>();
        tankerImage = tankerButton.GetComponent<Image>();

        Time.timeScale = 1.0f;
        StartCoroutine(IncrementGold());
        panelWin.SetActive(false);
        panelLose.SetActive(false);

        
        doneButton.onClick.AddListener(DoneSetting);    
        quitButton.onClick.AddListener(GoToMainMenu);
    }

    void Update()
    {
        goldText.text = "Gold: " + gold.ToString();

        UpdateButtonCooldown(infantryButton, infantryImage, lastInfantrySpawnTime, infantryCooldown);
        UpdateButtonCooldown(archerButton, archerImage, lastArcherSpawnTime, archerCooldown);
        UpdateButtonCooldown(mageButton, mageImage, lastMageSpawnTime, mageCooldown);
        UpdateButtonCooldown(tankerButton, tankerImage, lastTankerSpawnTime, tankerCooldown);
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
    public void SelectTanker()
    {
        unitToSpawn = tankerPrefab;
        isUnitSelected = true;
    }

    public void SelectMage()
    {
        unitToSpawn = magePrefab;
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
        else if (unitToSpawn == magePrefab)
        {
            if (Time.time >= lastMageSpawnTime + mageCooldown && CanSpawnUnit())
            {
                lastMageSpawnTime = Time.time;
                return true;
            }
        }
        else if (unitToSpawn == tankerPrefab)
        {
            if (Time.time >= lastTankerSpawnTime + tankerCooldown && CanSpawnUnit())
            {
                lastTankerSpawnTime = Time.time;
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
        else if(unitToSpawn == magePrefab)
        {
            return 25;
        }
        else if(unitToSpawn == tankerPrefab)
        {
            return 12;
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
            gold += 3;
        }
    }
    private void UpdateButtonCooldown(Button button, Image buttonImage, float lastSpawnTime, float cooldown)
    {
        float timeSinceLastSpawn = Time.time - lastSpawnTime;

        if (timeSinceLastSpawn >= cooldown)
        {
            button.interactable = true;
            buttonImage.color = Color.white; // Trở lại màu trắng khi có thể bấm
        }
        else
        {
            button.interactable = false;
            buttonImage.color = Color.gray; // Đổi sang màu xám khi đang cooldown
        }
    }
    public void ToggleSettingPanel()
    {
        if (settingPanel != null)
        {
            bool isActive = settingPanel.activeSelf;
            settingPanel.SetActive(!isActive);

            if (isActive)
            {
                Time.timeScale = 1.0f; // Tiếp tục trò chơi khi đóng bảng cài đặt
            }
            else
            {
                Time.timeScale = 0; // Dừng trò chơi khi mở bảng cài đặt
            }
        }
    }

    public void DoneSetting()
    {
        ToggleSettingPanel(); // Tắt bảng cài đặt và tiếp tục trò chơi
    }

}
