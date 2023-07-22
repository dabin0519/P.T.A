using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera _startCam;
    [SerializeField] private CinemachineVirtualCamera[] _cams;
    [SerializeField] private Animator _camAnim;
    
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
                _cams[i].Priority = 10;
            }
            else
            {
                _cams[i].Priority = 5;
            }
        }
    }

    public void SwitchCam(int value)
    {
        switch (value)
        {
            case 0:
                _camAnim.Play("PlayerCam");
                break;
            case 1:
                _camAnim.Play("Stage1Cam");
                break;
            case 2:
                _camAnim.Play("Stage2Cam");
                break;
            case 3:
                _camAnim.Play("Stage3Cam");
                break;
            default:
                Debug.LogWarning("This dosent have camera this num");
                break;
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
