using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] private StageDataSO[] _stageDatas;
    [SerializeField] private Transform _playerVisualTrm;
    [SerializeField] private Stage[] _stages;

    public Vector3 ClampMaxPos;
    public Vector3 ClampMinPos;

    private int _stageNum = 0;
    private bool _oneCall;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("�̰� �ΰ��ε���?");
        }
    }

    public void ChagneStage(int value)
    {
        _stageNum = value;
        _oneCall = false;
        Debug.LogWarning("Ȥ�ö� ī�޶� �۾��� �̻��ϴٸ� ���� stage�� �迭 �������� �ִ��� Ȯ���غ�");
        if(_stageNum == _stages.Length - 1)
            CameraManager.Instance.SwitchCam(0);
        else
            CameraManager.Instance.SwitchCam(value + 1);
    }

    private void Update()
    {
        if (!_oneCall)
        {
            _playerVisualTrm.position = _stageDatas[_stageNum].PlayerPos;
            ClampMaxPos = _stageDatas[_stageNum].ClampMaxPos;
            ClampMinPos = _stageDatas[_stageNum].ClampMinPos;
            _stages[_stageNum].CallStage();
            _oneCall = true;
        }
    }
}
