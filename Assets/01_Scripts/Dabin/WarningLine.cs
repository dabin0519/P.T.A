using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    [SerializeField, Tooltip("�и� ������ ��ٸ��� �ð�")] private float waitBlinkTime;
    [SerializeField, Tooltip("�Ѿ� ��� ������ ��ٸ��� �ð�")] private float waitShootTime;

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
        Debug.Log("shoot ȣ���");
        enemyMove.isShooting = false;
        enemyMove.Cooling();
        isCall = false;
        //�Ѿ� �߻�!!
    }

    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(waitBlinkTime);
        anim.SetBool("isBlink", true);
        yield return new WaitForSeconds(waitShootTime);
        Shoot();
    }
}