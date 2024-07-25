using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorConfig", menuName = "GameConfiguration/WarriorConfig", order = 1)]
public class WarriorSCO : ScriptableObject
{
    public float MaxHP;
    public float ATK;
    public float SPD;
    public float AS;
}
