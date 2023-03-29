using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private EnemyMove enemyMove;

    private void Awake()
    {
        enemyMove = GetComponentInParent<EnemyMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyMove.isMove = false;
            enemyMove.target = collision.transform;
            enemyMove.inRange = true;   
            enemyMove.hotZone.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
