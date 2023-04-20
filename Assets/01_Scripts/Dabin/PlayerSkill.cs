using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private float stopTime;

    [HideInInspector] public bool IsStop;
    [HideInInspector] public bool IsParry;
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
            StartCoroutine(StopTimer(stopTime));
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
}
