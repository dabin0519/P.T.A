using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemySO _enemyData;
    [SerializeField] private Transform _shootPos;
    [SerializeField] private Transform _playerTrm;

    private LineRenderer _lineRenderer;
    private Animator _enemyAnim;
    private EnemyAI _enemyAI;
    private Player _player;
    private Vector2 _target;

    private void Awake()
    {
        _lineRenderer = transform.parent.Find("LineRenderer").GetComponent<LineRenderer>();
        _enemyAnim = transform.parent.Find("Visual").GetComponent<Animator>();
        _player = _playerTrm.GetComponent<Player>();
        _enemyAI = transform.parent.GetComponent<EnemyAI>();
    }

    public void Attack()
    {
        StartCoroutine(GunAttack());
    }

    private IEnumerator GunAttack()
    {
        Debug.Log("�ڷ�ƾ ȣ��");
        yield return new WaitForSeconds(_enemyData.AttackCoolTime);
        Debug.Log("���� ����");
        _enemyAnim.SetTrigger("");
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, _shootPos.position);
        _target.x = _playerTrm.position.x;
        _target.y = _shootPos.position.y;
        _lineRenderer.SetPosition(1, _target);
        ChangeColor(Color.red);
        yield return new WaitForSeconds(0.2f);
        ChangeColor(Color.white);
        yield return new WaitForSeconds(0.2f);
        _enemyAnim.SetTrigger("isAttack");

        if (_player.GetState() == PlayerState.Parry)
        {
            _player.SetState(PlayerState.Counter);
        }
        else
        {
            _player.SetState(PlayerState.Die);
        }
        _lineRenderer.enabled = false;
        _enemyAI.SetState(State.Chase);
    }

    private void ChangeColor(Color lineColor)
    {
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
    }
}
