using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageMain2 : Stage
{
    public UnityEvent OnStage;

    public override void CallStage()
    {
        OnStage?.Invoke();
    }

    public override void EndDialogue()
    {
        
    }
}
