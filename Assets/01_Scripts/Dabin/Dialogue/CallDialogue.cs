using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogue : MonoBehaviour
{
    [SerializeField] private DialogueSO[] _dialogueSOs;

    public void OnDialogue()
    {
        DialogueManger.Instance.OnText(_dialogueSOs);
    }
}
