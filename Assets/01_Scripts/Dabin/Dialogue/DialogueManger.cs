using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManger : MonoBehaviour
{
    public static DialogueManger Instance;

    [SerializeField] private Dialogue _dialogue;
    private Player _player;

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

    public void OnText(DialogueSO[] dialogueSOs, CallDialogue callObject)
    {
        StartCoroutine(OnDialogue(dialogueSOs, callObject));
    }

    private IEnumerator OnDialogue(DialogueSO[] dialogueSOs, CallDialogue callObject)
    {
        _dialogue.gameObject.SetActive(true);
        foreach(DialogueSO d in dialogueSOs)
        {
            _dialogue.ShowText(d);
            yield return new WaitUntil(() => IsEnd);
            IsEnd = false;
        }
        callObject.IsEnd = true;
        _dialogue.gameObject.SetActive(false);
    }
}
