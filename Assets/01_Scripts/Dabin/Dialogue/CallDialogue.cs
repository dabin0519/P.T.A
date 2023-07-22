using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallDialogue : MonoBehaviour
{
    [SerializeField] private DialogueSO[] _dialogueSOs;
    [HideInInspector] public bool IsEnd;

    public UnityEvent DialogueEnd;

    public void OnDialogue()
    {
        DialogueManger.Instance.OnText(_dialogueSOs, this);
    }

    private void Update()
    {
        if (IsEnd)
        {
            DialogueEnd?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
