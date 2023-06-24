using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTransition : MonoBehaviour
{
    public List<DialogueDecision> Decisions;
    //public CommonAIState NextState;

    public void SetUp(Transform enemyRoot)
    {
        Decisions.ForEach(d => d.SetUP(enemyRoot));
    }

    public bool CheckTransition()
    {
        bool result = false;
        foreach (DialogueDecision decision in Decisions)
        {
            result = decision.MakeADecision();
            if (decision.IsReverse)
                result = !result;
            if (result == false)
                break;
        }
        return result;
    }
}
