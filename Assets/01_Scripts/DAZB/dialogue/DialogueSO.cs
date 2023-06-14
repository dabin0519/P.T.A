using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Contents;
}
