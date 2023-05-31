using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyHealthData")]
public class EnemyHealthSO : ScriptableObject
{
    public string Name;
    public int Health;
    public bool IsDie;
    public int IncreaseGage;
}
