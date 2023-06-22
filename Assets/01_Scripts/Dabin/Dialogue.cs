using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text _nickName;
    [SerializeField] private TMP_Text _contents;
    [SerializeField] private GameObject _dialoguePanel;

    private AudioSource _audio;

    private void Start() 
    {
        _audio = _contents.GetComponent<AudioSource>();
        _contents.maxVisibleCharacters = 0;
        DOTween.To(x => _contents.maxVisibleCharacters = (int)x, 0f, _contents.text.Length, 1f).SetEase(Ease.Linear);
    }

    public void ShowText(DialogueSO dialogueSO)
    {
        StartCoroutine(WriteContents(dialogueSO));
    }

    private IEnumerator WriteContents(DialogueSO dialogueSO)
    {
        _nickName.text = dialogueSO.Name;
        _contents.text = dialogueSO.Contents;
        _contents.maxVisibleCharacters = 0;
        DOTween.To(x => _contents.maxVisibleCharacters = (int)x, 0f, _contents.text.Length, dialogueSO.TextWritingTime).SetEase(Ease.Linear);
        _audio.Play();
        yield return new WaitForSeconds(1f);
        _audio.Stop();

        if (dialogueSO.IsEnd)
        {
            DisplayDialogue(false);
        }
    }

    public void DisplayDialogue(bool value)
    {
        Image panel = gameObject.GetComponent<Image>();
        gameObject.SetActive(value);

        if (value) //켜질때
        {
            panel.DOFade(255, 1f);
        }
        else //꺼질때
        {
            panel.DOFade(0, 1f);
        }
    }
}
