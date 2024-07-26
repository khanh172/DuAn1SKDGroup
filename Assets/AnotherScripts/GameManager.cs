using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject infantryPrefab;
    public GameObject archerPrefab;
    public UnitSpawner unitSpawner;
    private GameObject unitToSpawn;

    void Update()
    {
        // Kiểm tra nếu đơn vị đang được chọn và người chơi click chuột trái
        if (unitToSpawn != null && Input.GetMouseButtonDown(0))
        {
            Vector2 spawnPosition = GetSpawnPosition();
            unitSpawner.SpawnUnit(unitToSpawn, spawnPosition);
            unitToSpawn = null; // Reset lại sau khi spawn
        }
    }

    // Phương thức này cần phải là public để có thể gọi từ OnClick
    public void SelectInfantry()
    {
        unitToSpawn = infantryPrefab;
    }

    // Phương thức này cần phải là public để có thể gọi từ OnClick
    public void SelectArcher()
    {
        unitToSpawn = archerPrefab;
    }

    private Vector2 GetSpawnPosition()
    {
        // Lấy vị trí chuột trong thế giới 2D
        Vector3 position3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position2D = new Vector2(position3D.x, position3D.y);

        return position2D;
    }
}
