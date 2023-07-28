using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera _startCam;
    [SerializeField] private CinemachineVirtualCamera[] _cams;
    [SerializeField] private SpriteRenderer _panel;
    //[SerializeField] private Animator _camAnim;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("¹®Á¦ »ý±è (CameraManager)");
        }
    }

    public void SwitchCam(int value)
    {
        _panel.gameObject.SetActive(true);
        StartCoroutine(ActiveCoroutine());
        for(int i = 0; i < _cams.Length; i++)
        {
            if (i == value)
            {
                _cams[i].Priority = 10;
            }
            else
                _cams[i].Priority = 5;
        }
    }

    private IEnumerator ActiveCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _panel.gameObject.SetActive(false);
    }

    public void ClearPlayerCam(CinemachineVirtualCamera vcam)
    {
        _cams[0] = vcam;
    }

    public void CameraShake()
    {

    }

    public void CameraZoomIn()
    {

    }

    public void CameraZoomOut()
    {

    }

    public void CameraFadeIn()
    {

    }

    public void CameraFadeOut()
    {

    }
}
