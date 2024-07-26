using UnityEngine;

[CreateAssetMenu(fileName = "New Soldier Stats", menuName = "Soldier Stats")]
public class SoldierStats : ScriptableObject
{
    public string soldierName;
    public float attackRange;
    public float damage;
    public float moveSpeed;
    public float attackSpeed;
    public float health;
}
