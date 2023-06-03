using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    //[SerializeField] GameObject enemyParent;
    public void StopTime() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var stopTimeEnemies in enemies ) {
            Animator anim = stopTimeEnemies.GetComponent<Animator>();
            EnemyAI ai = stopTimeEnemies.GetComponent<EnemyAI>();
            //EnemyAI에 있는 moveSpeed 접근해서 속도를 0으로
            anim.speed = 0;
        }
    }
}
