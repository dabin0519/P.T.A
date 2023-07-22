using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _passione;
    [SerializeField] private float _fadeTime;

    private void Start()
    {
        StartCoroutine(IntroCoroutine());
    }

    private IEnumerator IntroCoroutine()
    {
        _passione.color = new Color(_passione.color.r, _passione.color.g, _passione.color.b, 0);
        _passione.DOFade(1, _fadeTime);
        yield return new WaitForSeconds(2f);
        _passione.DOFade(0, _fadeTime);
        Debug.LogError("game text");
    }
}