using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorConfig", menuName = "GameConfiguration/WarriorConfig", order = 1)]
public class WarriorSCO : ScriptableObject
{
    public int MaxHP;
    public int ATK;
    public int SPD;
    public int AS;
}
