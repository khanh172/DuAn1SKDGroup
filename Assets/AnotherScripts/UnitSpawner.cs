using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public Rect spawnArea; // Khu vực cho phép triệu hồi lính

    void Start()
    {
        spawnArea = new Rect(-7.22f, -4.18f, 1.6f, 5.12f);
    }
    public void SpawnUnit(GameObject unitPrefab, Vector2 position)
    {
        // Kiểm tra xem vị trí có nằm trong khu vực cho phép không
        if (IsPositionInSpawnArea(position))
        {
            Instantiate(unitPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Position is out of spawn area.");
        }
    }

    public bool IsPositionInSpawnArea(Vector2 position)
    {
        // Kiểm tra xem vị trí có nằm trong khu vực cho phép không
        return position.x >= spawnArea.xMin && position.x <= spawnArea.xMax && position.y >= spawnArea.yMin && position.y <= spawnArea.yMax;
    }
}
