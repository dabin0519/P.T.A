using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StageData")]
public class StageDataSO : ScriptableObject
{
    public Vector3 PlayerPos;
    public Vector3 ClampMinPos;
    public Vector3 ClampMaxPos;
}
