using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallDialogue : MonoBehaviour
{
    [SerializeField] private DialogueSO[] _dialogueSOs;
    [SerializeField] private bool IsActiveFalse = false;
    [HideInInspector] public bool IsEnd;

    public UnityEvent DialogueEnd;

    public void OnDialogue()
    {
        DialogueManger.Instance.OnText(_dialogueSOs, this);
    }

    private void Update()
    {
        if (IsEnd && IsActiveFalse)
        {
            DialogueEnd?.Invoke();
            gameObject.SetActive(false);
        }
        else if (IsEnd)
        {
            DialogueEnd?.Invoke();
        }
    }
}
