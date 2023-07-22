using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    [SerializeField] Transform _playerPos;
    [SerializeField] GameObject ButtonB;
    [SerializeField] LayerMask _layer;
    [SerializeField] EnemyAI _notCheck;

    private Animator _anim;
    private Player _player;

    int _delayTime = 15;

    private void Awake()
    {
        ButtonB.SetActive(false);
        _player = GetComponentInParent<Player>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (_notCheck._skillUse)
        {
            ButtonB.SetActive(true);
            ButtonB.transform.position = new Vector2(_playerPos.position.x, _playerPos.position.y + 0.5f);

            if (Input.GetKeyDown(KeyCode.E))
                StartCoroutine("SkillAssas");
        }
        else
            ButtonB.SetActive(false);
    }
    IEnumerator SkillAssas()
    {
        _player.SetState(PlayerState.Attack);
        _anim.SetTrigger("Attack");
        yield return new WaitForSeconds(_delayTime);
    }
}
