using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera _startCam;
    [SerializeField] private CinemachineVirtualCamera[] _cams;
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
        for(int i = 0; i < _cams.Length; i++)
        {
            if (i == value)
                _cams[i].Priority = 10;
            else
                _cams[i].Priority = 5;
        }
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
