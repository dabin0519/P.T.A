using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    //[SerializeField] GameObject _EnemyTrm;
    //EnemyAI _enemy;
    [SerializeField] GameObject _ghostEffectPrf;
    [SerializeField] float _spawnDelay;
    private bool isTimeStop;

    private void Start() {
        //_enemy = _EnemyTrm.GetComponentInChildren<EnemyAI>();
        StartCoroutine(GhostEftSpawn());
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && isTimeStop == false) {
            StopTime();
        }

        else if (Input.GetKeyDown(KeyCode.E) && isTimeStop == true) {
            StartTime();
        }
    }
    //[SerializeField] GameObject enemyParent;
    public void StopTime() {
        isTimeStop = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++) {
            Animator anim = enemies[i].GetComponentInChildren<Animator>();
            Rigidbody2D rigid = enemies[i].GetComponent<Rigidbody2D>();
            EnemyAI ai = enemies[i].GetComponent<EnemyAI>();
            ai.SaveState();
            ai.SetState(State.TimeStop);
            ai._isTimeStop = true;
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            //EnemyAI에 있는 moveSpeed 접근해서 속도를 0으로
            anim.speed = 0;
        }
    }

    public void StartTime() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var stopTimeEnemies in enemies ) {
            Animator anim = stopTimeEnemies.GetComponentInChildren<Animator>();
            Rigidbody2D rigid = stopTimeEnemies.GetComponent<Rigidbody2D>();
            EnemyAI ai = stopTimeEnemies.GetComponent<EnemyAI>();
            ai.StartCor();
            ai._isTimeStop = false;
            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            //EnemyAI에 있는 moveSpeed 접근해서 원래 속도로
            anim.speed = 1;
            isTimeStop = false;
        }
    }

    private IEnumerator GhostEftSpawn() {
        while (true) {
            if (isTimeStop == true) {
                Instantiate(_ghostEffectPrf, transform.position, transform.rotation);
                yield return new WaitForSeconds(_spawnDelay);
            }
            yield return null;
        }
    }


}
