using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [HideInInspector] public int _attackCount;

    private Animator _anim;
    private Player _player;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _player = GetComponentInParent<Player>();
        _attackCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _player.GetState() != PlayerState.Parry)
        {
            _player.SetState(PlayerState.Parry);
            _anim.SetTrigger("Parry");
        }
        if(Input.GetMouseButtonDown(0) && _attackCount != 0 && _player.GetState() != PlayerState.Parry)
        {
            _player.SetState(PlayerState.Attack);
            _attackCount--;
            _anim.SetTrigger("Attack");
        }

        if (_player.GetState() == PlayerState.Counter)
        {
            _anim.SetBool("isParry", true);
            _attackCount++;
        }
    }

    public bool ParryCheck() 
    {
        return _player.GetState() == PlayerState.Parry;
    }

    public void EndAnimation()
    {
        _player.SetState(PlayerState.Move);
    }
}
