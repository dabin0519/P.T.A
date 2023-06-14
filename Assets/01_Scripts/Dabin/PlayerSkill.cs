using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Animator _anim;
    private Player _player;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _player.GetState() != PlayerState.Parry)
        {
            _player.SetState(PlayerState.Parry);
            _anim.SetTrigger("Parry");
        }
        if(Input.GetMouseButtonDown(0) && _player.GetState() != PlayerState.Parry)
        {
            Debug.Log("PressAttack");
            _player.SetState(PlayerState.Attack);
            _anim.SetTrigger("Attack");
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
