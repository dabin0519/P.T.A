using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private EnemyAI _enemyAI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _enemyAI = collision.gameObject.GetComponent<EnemyAI>();
            _enemyAI.SetState(State.Die);
        }
    }
}
