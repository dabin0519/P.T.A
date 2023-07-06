using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    [SerializeField] private Transform _enemyPos;
    [SerializeField] private GameObject _skillTxt;

    private Animator _anim;
    private EnemyAI _enemyAI;
    private Player _player;
    private Transform _playerPos;

    private Vector2 _player_x;
    private Vector2 _enemy_x;
    private bool _isSkill;
    private int _delayTime = 15;

    private void Awake()
    {
        _isSkill = false;
        _skillTxt.SetActive(false);
        _player = GetComponentInParent<Player>();
        _anim = GetComponentInChildren<Animator>();
        _playerPos = _player.transform.Find("Visual").transform;
        _enemyAI = _enemyPos.GetComponent<EnemyAI>();
    }

    void Update()
    {
        GetPos();
        UseSkill();
        ButtonSetActive();
    }

    void GetPos()
    {
        if (_playerPos != null)
            _player_x = new Vector2(_playerPos.position.x, 0);

        if (_enemyPos != null)
        {
            _enemy_x = new Vector2(_enemyPos.position.x, 0);
            _skillTxt.SetActive(true);
        }
        else
        {
            _skillTxt.SetActive(false);
            _isSkill = false;
        }
    }

    void UseSkill()
    {
        if (!_enemyAI._isCheckPlayer)
        {
            if (_isSkill && Input.GetKeyDown(KeyCode.E))
                StartCoroutine("SkillAssas");
        }
        else
            _skillTxt.SetActive(false);
    }

    void ButtonSetActive()
    {
        if (Vector2.Distance(_player_x, _enemy_x) < 3f)
        {
            _isSkill = true;
            if (!_enemyAI._isCheckPlayer)
                _skillTxt.transform.position = new Vector2(_playerPos.position.x, _playerPos.position.y + 0.5f);
            else
                _skillTxt.SetActive(false);
        }
        else
        {
            _skillTxt.SetActive(false);
            _isSkill = false;
        }
    }

    IEnumerator SkillAssas()
    {
        _player.SetState(PlayerState.Attack);
        _anim.SetTrigger("Attack");
        yield return new WaitForSeconds(_delayTime);
    }
}
