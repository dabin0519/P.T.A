using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    bool isTimeStop;
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
        foreach(var stopTimeEnemies in enemies ) {
            Animator anim = stopTimeEnemies.GetComponentInChildren<Animator>();
            Rigidbody2D rigid = stopTimeEnemies.GetComponent<Rigidbody2D>();
            EnemyAI ai = stopTimeEnemies.GetComponent<EnemyAI>();
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
            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            //EnemyAI에 있는 moveSpeed 접근해서 원래 속도로
            anim.speed = 1;
            isTimeStop = false;
        }
    }
}
