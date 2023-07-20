using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    [SerializeField] Transform _enemyPos;
    [SerializeField] Transform _playerPos;
    [SerializeField] GameObject ButtonB;
    [SerializeField] LayerMask _layer;

    private Animator _anim;
    private EnemyAI _enemyAI;
    private Player _player;

    Vector2 _player_x;
    Vector2 _enemy_x;

    bool _isSkill;
    int _delayTime = 15;

    private void Awake()
    {
        _isSkill = false;
        ButtonB.SetActive(false);
        _player = GetComponentInParent<Player>();
        _anim = GetComponentInChildren<Animator>();
        _enemyAI = _enemyPos.GetComponent<EnemyAI>();
    }

    void Update()
    {
        //GetPos();
        //ButtonSetActive();
        UseSkill();

        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y -0.7f),Vector2.right, 2f, _layer);

        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 1f),Vector2.right * 2, Color.red);

        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.7f), Vector2.left, 2f, _layer);

        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.left * 2, Color.red);


        if (hitLeft)
        {
            if (!_enemyAI._isCheckPlayer)
            {
                _isSkill = true;
                ButtonB.SetActive(true);
                ButtonB.transform.position = new Vector2(_playerPos.position.x, _playerPos.position.y + 0.5f);
            }
            else
            {
                _isSkill = false;
                ButtonB.SetActive(false);
            }

        }
        else
        {
            ButtonB.SetActive(false);
            _isSkill = false;
        }

        if (hitRight)
        {
            if (!_enemyAI._isCheckPlayer)
            {
                _isSkill = true;
                ButtonB.SetActive(true);
                ButtonB.transform.position = new Vector2(_playerPos.position.x, _playerPos.position.y + 0.5f);
            }
            else
            {
                _isSkill = false;
                ButtonB.SetActive(false);
            }

        }
        else
        {
            ButtonB.SetActive(false);
            _isSkill = false;
        }
    }


    void GetPos()
    {
        //if (_playerPos != null)
        //    _player_x = new Vector2(_playerPos.position.x, 0);

        //if (_enemyPos != null)
        //{
        //    _enemy_x = new Vector2(_enemyPos.position.x, 0);
        //    ButtonB.SetActive(true);
        //}
        //else
        //{
        //    ButtonB.SetActive(false);
        //    _isSkill = false;
        //}
    }

    void UseSkill()
    {
        if (!_enemyAI._isCheckPlayer)
        {
            if (_isSkill && Input.GetKeyDown(KeyCode.E))
                StartCoroutine("SkillAssas");
        }
    }

    void ButtonSetActive()
    {
        //if (Vector2.Distance(_player_x, _enemy_x) < 3f)
        //{
            
        //    _isSkill = true;
        //    if (!_enemyAI._isCheckPlayer)
        //        ButtonB.transform.position = new Vector2(_playerPos.position.x, _playerPos.position.y + 0.5f);
        //    else
        //        ButtonB.SetActive(false);
        //}
        //else
        //{
        //    ButtonB.SetActive(false);
        //    _isSkill = false;
        //}
    }

    IEnumerator SkillAssas()
    {
        //ButtonB.SetActive(false);
        _player.SetState(PlayerState.Attack);
        _anim.SetTrigger("Attack");
        //_isSkill = false;
        yield return new WaitForSeconds(_delayTime);
    }
}
