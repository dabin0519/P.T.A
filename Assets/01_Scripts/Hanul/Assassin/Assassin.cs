using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    [SerializeField] Transform _enemyPos;
    [SerializeField] Transform _playerPos;
    [SerializeField] GameObject ButtonB;

    private Animator _anim;
    private EnemyAI _enemyAI;
    private Player _player;

    Vector2 _player_x;
    Vector2 _enemy_x;
    bool _isSkill;

    private void Awake()
    {
        _isSkill = false;
        ButtonB.SetActive(false);
        _enemyAI = _enemyPos.GetComponent<EnemyAI>();
        _player = GetComponentInParent<Player>();
        _anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {

    }
    void Update()
    {
        if(_playerPos != null)
        {
            _player_x = new Vector2(_playerPos.position.x, 0);
        }
        if(_enemyPos != null)
        {
            _enemy_x = new Vector2(_enemyPos.position.x, 0);
        }

        if (Vector2.Distance(_player_x, _enemy_x) < 3f)
        {
            _isSkill = true;

            if (!_enemyAI._isCheckPlayer)
            {
                ButtonB.SetActive(true);
                ButtonB.transform.position = new Vector2(_playerPos.position.x, _playerPos.position.y + 0.5f);
            }
        }
        else
        {
            _isSkill = false;
            ButtonB.SetActive(false);
        }

        if (!_enemyAI._isCheckPlayer)
        {
            if (_isSkill && Input.GetKeyDown(KeyCode.E))
            {
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
