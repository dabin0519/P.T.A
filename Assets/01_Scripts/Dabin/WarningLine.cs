using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    [SerializeField, Tooltip("패링 전까지 기다리는 시간")] private float waitBlinkTime;
    [SerializeField, Tooltip("총알 쏘기 전까지 기다리는 시간")] private float waitShootTime;

    private EnemyMove enemyMove;
    private Animator anim;
    private bool isCall;

    private void Start()
    {
        enemyMove = GetComponentInParent<EnemyMove>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyMove.isShooting && !isCall)
        {
            isCall = true;
            StartCoroutine(ShootCoroutine());
        }
    }

    private void Shoot()
    {
        gameObject.SetActive(false);
        Debug.Log("shoot 호출됨");
        enemyMove.isShooting = false;
        enemyMove.Cooling();
        isCall = false;
        //총알 발사!!
    }

    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(waitBlinkTime);
        anim.SetBool("isBlink", true);
        yield return new WaitForSeconds(waitShootTime);
        Shoot();
    }
}