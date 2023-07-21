using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemySO _enemyData;
    [SerializeField] private Transform _shootPos;
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private LayerMask _playerLayer;

    private LineRenderer _lineRenderer;
    private Animator _enemyAnim;
    private EnemyAI _enemyAI;
    private Player _player;
    private Vector2 _target;
    private Transform _playerVisualTrm;

    private void Awake()
    {
        _lineRenderer = transform.parent.Find("LineRenderer").GetComponent<LineRenderer>();
        _enemyAnim = transform.parent.GetComponent<Animator>();
        _player = _playerTrm.GetComponent<Player>();
        _enemyAI = transform.parent.parent.GetComponent<EnemyAI>();
        _playerVisualTrm = _playerTrm.Find("Visual").transform;
    }

    public void Attack()
    {
        StartCoroutine(GunAttack());
    }
    
    public void StopAtkCor() {
        StopCoroutine(GunAttack());
    }

    private IEnumerator GunAttack()
    {
        yield return new WaitForSeconds(_enemyData.AttackCoolTime);
        if (_enemyAI._isTimeStop)   yield break;
        _enemyAnim.SetTrigger("");
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, _shootPos.position);
        _target.x = _playerVisualTrm.position.x;
        _target.y = _shootPos.position.y;
        _lineRenderer.SetPosition(1, _target);
        ChangeColor(Color.red);
        yield return new WaitForSeconds(0.2f);
        if (_enemyAI._isTimeStop) yield break;
        ChangeColor(Color.white);
        yield return new WaitForSeconds(0.2f);
        if (_enemyAI._isTimeStop) yield break;
        _enemyAnim.SetTrigger("isAttack");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _target - (Vector2)transform.position );
        Debug.DrawRay(transform.position, _target - (Vector2)transform.position, Color.blue, 1f);

        if (hit)
        {
            if (_player.GetState() == PlayerState.Parry)
            {
                _player.SetState(PlayerState.Counter);
            }
            else
            {
                _player.SetState(PlayerState.Die);
            }
        }

        Debug.Log("실행 안되지?");
        _lineRenderer.enabled = false;
        _enemyAI.SetState(State.Chase);
        yield return new WaitForSeconds(2);
        _enemyAI._isAtkWaitCool = false;
    }

    private void ChangeColor(Color lineColor)
    {
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
    }
}
