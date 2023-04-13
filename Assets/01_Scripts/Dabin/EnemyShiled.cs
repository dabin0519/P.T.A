using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShiled : MonoBehaviour
{
    [SerializeField] private float checkDistance;
    [SerializeField] private float attackDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform targetPos;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float backTime;

    private bool isAttack;
    private Vector3 rotation;

    private void Update()
    {
        if (isAttack)
            return;

        if (transform.position.x - targetPos.position.x < attackDistance && !(transform.position.x - targetPos.position.x < 0)) //사거리 안이면 움직임을 멈추고 공격 시작
        {
            isAttack = true;
            Attack();
        }
        else
        {
            ShootRay();
            Flip();
        }
    }

    private void Attack()
    {
        Debug.Log("사거리 안");
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("공격");
        isAttack = false;
    }

    private void ShootRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, checkDistance, playerLayer);
        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
                Follow();
        }
    }

    private void Follow()
    {
        Vector2 targetPosition = new Vector2(targetPos.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void Flip()
    {
        rotation = transform.eulerAngles;

        if (transform.position.x < targetPos.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

    }
}
