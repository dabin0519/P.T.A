using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyHealthData")]
public class EnemyHealthSO : ScriptableObject
{
    public int Health;
    public bool IsDie;
    public int IncreaseGage;
}
