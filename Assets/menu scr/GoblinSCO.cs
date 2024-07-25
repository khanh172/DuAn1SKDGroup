using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoblinConfig", menuName = "GameConfiguration/GoblinConfig", order = 1)]

public class GoblinSCO : ScriptableObject
{
    public float MaxHP;
    public float ATK;
    public float SPD;
    public float AS;
}
