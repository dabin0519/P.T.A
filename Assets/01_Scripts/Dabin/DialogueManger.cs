using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManger : MonoBehaviour
{
    public static DialogueManger Instance;

    [SerializeField] private DialogueSO[] _dialogues;
    [SerializeField] private Dialogue _dialogue;

    public bool IsEnd;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("문제 발생");
        }
    }

    public void OnText()
    {
        StartCoroutine(OnDialogue());
    }

    private IEnumerator OnDialogue()
    {
        foreach(DialogueSO d in _dialogues)
        {
            _dialogue.ShowText(d);
            yield return new WaitUntil(() => IsEnd);
            IsEnd = false;
        }
    }
}
