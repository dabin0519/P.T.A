using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleEnemyAttackManger : MonoBehaviour
{
    [SerializeField] private EnemySO _enemyData;
    [SerializeField] private Transform _playerTrm;

    private Animator _enemyAnim;
    private Player _player;
    private EnemyAI _enemyAI;
    // Start is called before the first frame update
    void Start()
    {
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
        //_isAttack = true;
        _enemyAnim.SetTrigger("isAttack");

        if (_player.GetState() == PlayerState.Parry)
        {
            //yield return new WaitForSeconds(_enemyData.AttackCoolTime);
        }
        else
        {
            _player.SetState(PlayerState.Die);
        }
        yield return new WaitForSeconds(_enemyData.AttackCoolTime);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.2f);
        _enemyAI.SetState(State.Chase);
    }
}
