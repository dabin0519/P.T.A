using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private CinemachineVirtualCamera _vcam;

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
