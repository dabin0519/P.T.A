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
            //���� ���̾Ʒα� �ٿ�;
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
        contents.text = "���� �� ��������...";
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
                dialogueSO.Contents = "������ �ʹ�";
                break;
            case 1:
                dialogueSO.Contents = "��¥ ������ �ʹ�";
                break;
            case 2:
                dialogueSO.Contents = "�� ��������...";
                break;
            case 15:
                dialogueSO.Contents = "�� ������ �޶��!!!";
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
