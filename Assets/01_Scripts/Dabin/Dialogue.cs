using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueSO[] _dialogues;

    public void NextDialogue(Action callback = null)
    {
        if(callback == null)
        {
            //다음 다이아로그 뛰워;
        }
        else
        {
            callback?.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue(() =>
            {

            });
        }
    }

    /*[SerializeField] TMP_Text nickName;
    [SerializeField] TMP_Text contents;
    [SerializeField] GameObject dialoguePanel;
    new AudioSource audio;
    int contentsCnt = 0;
    public DialogueSO dialogueSO;

    private void Start() {
        audio = contents.GetComponent<AudioSource>();
        nickName.text = dialogueSO.Name;
        contents.text = "내가 왜 여기있지...";
        contents.maxVisibleCharacters = 0;
        DOTween.To(x => contents.maxVisibleCharacters = (int)x, 0f, contents.text.Length, 1f).SetEase(Ease.Linear);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine("WriteContents");
        }
    }

    private IEnumerator WriteContents() {
            NextText();
            contents.text = dialogueSO.Contents;
            contents.maxVisibleCharacters = 0;
            DOTween.To(x => contents.maxVisibleCharacters = (int)x, 0f, contents.text.Length, 1f).SetEase(Ease.Linear);
            audio.Play();
            yield return new WaitForSeconds(1f);
            audio.Stop();
            
    }

    private void NextText() {
        switch (contentsCnt) {
            case 0:
                dialogueSO.Contents = "집가고 싶다";
                break;
            case 1:
                dialogueSO.Contents = "진짜 집가고 싶다";
                break;
            case 2:
                dialogueSO.Contents = "날 내보내줘...";
                break;
            case 15:
                dialogueSO.Contents = "날 내보내 달라고!!!";
                break;
        }
        contentsCnt++;
    }

    public void DisplayDialogue() {
        gameObject.SetActive(true);
    }

    public void UndisplayDialogue() {
        gameObject.SetActive(false);
    }*/
}
