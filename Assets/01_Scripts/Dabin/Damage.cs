using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<EnemyAI>().IsAttacked = true;
        }
    }
}
