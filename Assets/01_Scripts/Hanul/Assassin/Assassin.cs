using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    [SerializeField] Transform _enemyPos;
    [SerializeField] Transform _playerPos;

    private Animator _anim;
    private EnemyAI _enemyAI;
    private Player _player;

    Vector2 _player_x;
    Vector2 _enemy_x;
    bool _isSkill;
    bool _isGetPos;

    private void Awake()
    {
        _isSkill = false;
        _isGetPos = true;
        _enemyAI = _enemyPos.GetComponent<EnemyAI>();
        _player = GetComponentInParent<Player>();
        _anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {

    }
    void Update()
    {
        _player_x = new Vector2(_playerPos.position.x, 0);

        if(_isGetPos)
        {
            _enemy_x = new Vector2(_enemyPos.position.x, 0);
        }
        Debug.Log(Vector2.Distance(_player_x, _enemy_x));
        if (Vector2.Distance(_player_x, _enemy_x) < 5f)
        {
            _isSkill = true;
        }
        else
        {
            _isSkill = false;
        }

        if (!_enemyAI._isCheckPlayer)
        {
            if (_isSkill && Input.GetKeyDown(KeyCode.E))
            {
                _isGetPos = false;
                StartCoroutine("SkillAssas");

            }

        }
    }

    IEnumerator SkillAssas()
    {
        _player.SetState(PlayerState.Attack);
        _anim.SetTrigger("Attack");
        yield return new WaitForSeconds(10f);
    }
}
