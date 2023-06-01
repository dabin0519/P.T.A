using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemySO : ScriptableObject
{
    [Header("Data")]
    public string Name;
    public int Health;

    [Header("AI")]
    public float Speed;
    public float ViewDistance;
    [Tooltip("?ǥ���� !ǥ�� �Ѿ�� �ð�")] public float AlretTime;
    public float AttackDistance;
    public float AttackCoolTime;
    public bool IsDie;
    
    [Header("Patrol")]
    [Tooltip("�� ģ���� ���ָ� �¿�� �����δ�.")]
    public bool IsPatrol;
}
