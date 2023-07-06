using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private GameObject _atkEffect;
    private GameObject[] objs = new GameObject[20];
    private Vector3 dir;
    private TimeStop _timeStop;
    private int index = 0;

    private void Start() {
        _timeStop = GetComponentInParent<TimeStop>();
    }
    
    private void Update() {
        if (_timeStop.isTimeStop == false && objs != null) {
            for (int i = 0; i < objs.Length; i++) {
                Destroy(objs[i]);
            }
            index = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        if (collision.CompareTag("Enemy")) {
            EnemyAI enemyAI = collision.GetComponent<EnemyAI>();
            if (enemyAI._isTimeStop == true) {
                int rand = Random.Range(0, 180);
                dir = new Vector3(0, 0, rand);
                objs[index] =  Instantiate(_atkEffect, collision.transform.position, Quaternion.Euler(dir));
                index++;
            }
            enemyAI.IsAttacked = true;
        }
    }
}
