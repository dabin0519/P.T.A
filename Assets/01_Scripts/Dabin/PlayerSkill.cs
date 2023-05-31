using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : Player
{
    [SerializeField] private float _stopTime;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && State != StateEnum.Parry)
        {
            State = StateEnum.Parry;
            _anim.SetTrigger("Parry");
        }
        if(Input.GetMouseButtonDown(0) && State != StateEnum.Parry)
        {
            Debug.Log("PressAttack");
            State = StateEnum.Attack;
            _anim.SetTrigger("Attack");
        }
    }

    public bool ParryCheck() 
    {
        return State == StateEnum.Parry;
    }

    public void EndAnimation()
    {
        State = StateEnum.Move;
    }
}
