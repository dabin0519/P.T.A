using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Stage3 : Stage
{
    [SerializeField] private Transform _playerTrm;

    public override void CallStage()
    {
        CameraManager.Instance.ClearPlayerCam(_playerTrm.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>());
        CameraManager.Instance.SwitchCam(0);
    }

    public override void EndDialogue()
    {
        
    }
}
