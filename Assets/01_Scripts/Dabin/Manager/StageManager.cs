using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] private StageDataSO[] _stages;
    [SerializeField] private Transform _playerVisualTrm;

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
            _playerVisualTrm.position = _stages[_stageNum].PlayerPos;
            _oneCall = true;
        }
    }
}
