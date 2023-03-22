using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMode
{
    Gun,
    Shiled,
    Knife
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyMode enemyMode;
    [SerializeField] private Transform[] patrol;
    [SerializeField] private Transform rayCastPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distance;

    private Vector3 dir = new Vector3(1, 0, 0);

    private void Start()
    {
        switch (enemyMode)
        {
            case EnemyMode.Gun:
                break;
            case EnemyMode.Shiled:
                break;
            case EnemyMode.Knife:
                break;
        }
    }

    private void Update()
    {
        if(transform.position.x > patrol[1].position.x)
        {
            dir = new Vector3(-1, 0, 0);
        }
        else if(transform.position.x < patrol[0].position.x)
        {
            dir = new Vector3(1, 0, 0);
        }

        Move();
    }

    private void FixedUpdate()
    {
    }

    /*private void OnDrawGizmos()
    {   
        RaycastHit2D hit;
        hit = Physics2D.Raycast(rayCastPos.position, Vector2.left, distance);

        if(hit.collider != null)
        {
            Debug.DrawRay(transform.position, hit.point, Color.green);
        }
        else
        {
            Debug.DrawRay(rayCastPos.p, transform.position + transform.right * distance, Color.red);
        }
        
    }*/

    private void Move()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;   
    }
}
