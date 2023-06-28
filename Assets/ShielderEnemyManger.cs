using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShielderEnemyManger : MonoBehaviour
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
        StartCoroutine(ShieldIEnumerator());
    }

    private IEnumerator ShieldIEnumerator()
    {
        _enemyAnim.SetTrigger("방어");
        Debug.Log("방패");
        _enemyAI.hp = 555;
        yield return new WaitForSeconds(_enemyData.AttackCoolTime);
        //yield return new WaitForSeconds(0.2f);
        _enemyAI.SetState(State.Chase);
        _enemyAI.hp = 1;
        yield return null;
    }
}
