using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueDecision : MonoBehaviour
{
    public bool IsReverse = false;

    public virtual void SetUP(Transform enemyRoot)
    {

    }

    public abstract bool MakeADecision();
}
