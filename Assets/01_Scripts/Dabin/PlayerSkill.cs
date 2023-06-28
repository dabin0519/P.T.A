using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Animator _anim;
    private Player _player;

    private bool canUseSkill
    {
        get
        {
            return _player.GetState() != PlayerState.Parry && _player.GetState() != PlayerState.Die;
        }
    }

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && canUseSkill)
        {
            _player.SetState(PlayerState.Parry);
            _anim.SetTrigger("Parry");
        }
        if(Input.GetMouseButtonDown(0) && canUseSkill)
        {
            Debug.Log("PressAttack");
            _player.SetState(PlayerState.Attack);
            _anim.SetTrigger("Attack");
        }

        if(Input.GetKeyDown(KeyCode.Z) && canUseSkill)
        {
            //_player.SetState(PlayerState.Attack);
            _anim.SetTrigger("Throw");
            GetComponentInChildren<ThrowingWeaponManger>().Throw(GetComponentInChildren<ThrowingWeaponManger>().weaponGameObject, 5f);
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
