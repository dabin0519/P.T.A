using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private GameObject _atkEffect;
    private Vector3 dir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        if (collision.CompareTag("Enemy")) {
            EnemyAI enemyAI = collision.GetComponent<EnemyAI>();
            if (enemyAI._isTimeStop == true) {
                int rand = Random.Range(0, 180);
                dir = new Vector3(0, 0, rand);
                Instantiate(_atkEffect, collision.transform.position, Quaternion.Euler(dir));
            }
            enemyAI.IsAttacked = true;

        }
    }

    public void StartAtkEffectAnimation() {
        
    }
}
