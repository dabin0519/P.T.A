using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueAction
{
    WaitSecond, //ÀÚµ¿ 
    WaitInteractable, //
    WaitClick
}

[CreateAssetMenu(menuName = "SO/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public string Name;
    public DialogueAction Action;
    [TextArea]
    public string Contents;
}
