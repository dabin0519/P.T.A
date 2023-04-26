using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private float _stopTime;
    [SerializeField] private int _attackTime;

    [HideInInspector] public bool IsStop;
    [HideInInspector] public bool IsParry;
    [HideInInspector] public bool IsCounter;
    [HideInInspector] public bool IsParryAnimation;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !IsParry)
        {
            IsParry = true;
            IsParryAnimation = true;
            _anim.SetTrigger("Parry");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            IsStop = true;
            StartCoroutine(StopTimer(_stopTime));
        }
        if(Input.GetMouseButtonDown(0) && !IsParry && _attackTime > 0)
        {
            _attackTime--;
            _anim.SetTrigger("Attack");
        }
    }

    private IEnumerator StopTimer(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);
        IsStop = false;
    }

    public void FinishParry()
    {
        IsParry = false;
    }

    public void FinishParryAnimation()
    {
        IsParryAnimation = false;
    }

    public void ParryCheck(bool isParry) 
    {
        if (!isParry)
            IsParryAnimation = false;

        _anim.SetBool("isParry", isParry);
        IsCounter = true;
        IsParry = false;
    }

    public void FinishCounter()
    {
        _anim.SetBool("isParry", false);
        _attackTime++;
        IsCounter = false;
        IsParryAnimation = false;
    }
}
