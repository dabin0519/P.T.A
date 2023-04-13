using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Patrolling,
        Alert,
        Chasing,
        Attacking
    }

    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float alertDuration = 1f;
    [SerializeField] private float attackDelay = 2f;

    private int currentWaypoint = 0;
    private Transform player;
    private State currentState = State.Patrolling;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                CheckForPlayer();
                break;
            case State.Alert:
                break;
            case State.Chasing:
                Chase();
                CheckForAttack();
                break;
            case State.Attacking:
                break;
        }
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        Vector2 target = waypoints[currentWaypoint].position;
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    private void CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, viewDistance, playerLayer);

        if (hit && hit.collider.CompareTag("Player"))
        {
            StartCoroutine(Alert());
        }
    }

    private IEnumerator Alert()
    {
        currentState = State.Alert;
        yield return new WaitForSeconds(alertDuration);
        currentState = Physics2D.Raycast(transform.position, player.position - transform.position, viewDistance, playerLayer) ? State.Chasing : State.Patrolling;
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void CheckForAttack()
    {
        if (Vector2.Distance(transform.position, player.position) < attackDistance)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        currentState = State.Attacking;
        transform.position = transform.position; // Stop moving
        yield return new WaitForSeconds(attackDelay);
        // Perform attack here (e.g. spawn a projectile or trigger an animation)
        currentState = State.Chasing;
    }
}
