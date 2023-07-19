using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera _startCam;
    [SerializeField] private CinemachineVirtualCamera[] _cams;
    
    private CinemachineVirtualCamera _currentCam;

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

    private void Start()
    {
        _currentCam = _startCam;

        for(int i = 0; i < _cams.Length; i++)
        {
            if(_cams[i] == _currentCam)
            {
                _cams[i].Priority = 20;
            }
            else
            {
                _cams[i].Priority = 10;
            }
        }
    }

    public void SwitchCam(CinemachineVirtualCamera newCam)
    {
        _currentCam = newCam;

        _currentCam.Priority = 20;
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
