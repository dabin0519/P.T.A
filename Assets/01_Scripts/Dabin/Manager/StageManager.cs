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
            Debug.LogError("이거 두개인데용?");
        }
    }

    public void ChagneStage(int value)
    {
        _stageNum = value;
        _oneCall = false;
    }

    private void Update()
    {
        if (!_oneCall)
        {
            _playerVisualTrm.position = _stageDatas[_stageNum].PlayerPos;
            _stages[_stageNum].CallStage();
            _oneCall = true;
        }
    }
}
