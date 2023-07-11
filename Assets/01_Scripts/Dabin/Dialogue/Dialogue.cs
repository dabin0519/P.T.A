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

    private Image _panel;
    private AudioSource _audio;
    private DialogueSO _dialogueSO;

    private void Start() 
    {
        _panel = GetComponent<Image>();
        _audio = _contents.GetComponent<AudioSource>();
        _contents.maxVisibleCharacters = 0;
        _nickName.color = new Color(255,255,255, 0);
    }

    public void ShowText(DialogueSO dialogueSO)
    {
        _dialogueSO = dialogueSO;
        DisplayDialogue(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(WriteContents(_dialogueSO));
            _contents.maxVisibleCharacters = _dialogueSO.Contents.Length;
            StartCoroutine(ClickSpace());
        }
    }

    private IEnumerator ClickSpace()
    {
        if (_dialogueSO.IsEnd)
        {
            DisplayDialogue(false);

            yield return new WaitForSeconds(1.2f);
            gameObject.SetActive(false);
        }
        else
        {
            DialogueManger.Instance.IsEnd = true;
        }
    }

    private IEnumerator WriteContents(DialogueSO dialogueSO)
    {
        _nickName.text = dialogueSO.Name;
        _contents.text = dialogueSO.Contents;
        _contents.maxVisibleCharacters = 0;
        DOTween.To(x => _contents.maxVisibleCharacters = (int)x, 0f, _contents.text.Length, dialogueSO.TextWritingTime).SetEase(Ease.Linear);
        //_audio.Play();
        yield return new WaitForSeconds(1f);
        //_audio.Stop();

        //Debug.Log();

        DialogueManger.Instance.IsEnd = true;
    }


    public void DisplayDialogue(bool value)
    {
        if (value) //켜질때
        {
            StartCoroutine(OnDialogue());
        }
        else //꺼질때
        {
            _panel.DOFade(0, 1f);
            _nickName.DOFade(0, 1f);
            _contents.DOFade(0, 1f);
        }
    }

    private IEnumerator OnDialogue()
    {
        gameObject.SetActive(true);
        //_panel.DOFade(0.7f, 1f);
        yield return new WaitForSeconds(1f);
        _nickName.color = new Color(255,255,255, 255);
        StartCoroutine(WriteContents(_dialogueSO));
    }
}
