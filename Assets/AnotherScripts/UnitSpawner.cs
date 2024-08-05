using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public Rect spawnArea; // Khu vực cho phép triệu hồi lính

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

    private bool IsPositionInSpawnArea(Vector2 position)
    {
        // Kiểm tra xem vị trí có nằm trong khu vực cho phép không
        return position.x >= spawnArea.xMin && position.x <= spawnArea.xMax && position.y >= spawnArea.yMin && position.y <= spawnArea.yMax;
    }
}
