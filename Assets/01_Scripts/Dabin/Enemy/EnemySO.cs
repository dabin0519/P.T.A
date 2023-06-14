using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemySO : ScriptableObject
{
    [Header("---------------------Data---------------------")]
    public EnemyEnum EnemyMode;
    public int Health;
    public RuntimeAnimatorController Controller;

    [Header("---------------------AI---------------------")]
    public float Speed;
    public float ViewDistance;
    [Tooltip("?표에서 !표로 넘어가는 시간")] public float AlretTime;
    public float AttackDistance;
    public float AttackCoolTime;
    public bool IsDie;
    
    [Header("---------------------Patrol---------------------")]
    [Tooltip("이 친구를 켜주면 좌우로 움직인다.")]
    public bool IsPatrol;
}
