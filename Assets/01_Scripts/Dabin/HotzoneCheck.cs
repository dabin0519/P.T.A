using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotzoneCheck : MonoBehaviour
{
    private EnemyMove enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
        enemyParent = GetComponentInParent<EnemyMove>();
    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnmeyAttack"))
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !enemyParent.follow)
        {
            inRange = false;
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
            gameObject.SetActive(false);
        }
    }
}
