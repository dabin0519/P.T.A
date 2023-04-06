using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShiled : MonoBehaviour
{
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform targetPos;
    [SerializeField] private float moveSpeed = 5f;

    private void Update()
    {
        ShootRay();
    }

    private void ShootRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, checkDistance, playerLayer);
        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
                Follow();
        }
        else
        {
            Debug.Log("Player?");
        }
    }

    private void Follow()
    {
        Vector2 targetPosition = new Vector2(targetPos.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
