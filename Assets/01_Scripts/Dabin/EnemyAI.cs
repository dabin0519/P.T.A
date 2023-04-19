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

    public bool isAttack = false;

    private Transform player;
    private State currentState = State.Patrolling;
    private bool isCheckPlayer;
    private int currentWaypoint = 0;
    private Vector2 target;

    private Animator _enemyAnim;
    private LineRenderer _lineRenderer;
    private PlayerMove _playerMove;

    private void Awake()
    {
        _enemyAnim = GetComponentInChildren<Animator>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale = new Vector3(-1f, 1f, 1f);
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

        target = new Vector2(waypoints[currentWaypoint].position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;

            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    private void CheckForPlayer()
    {
        if (isCheckPlayer)
        {
            currentState = State.Chasing;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, viewDistance, playerLayer);

        if (hit && hit.collider.CompareTag("Player"))
        {
            _enemyAnim.SetTrigger("isAlert");
            StartCoroutine(Alert());
        }
    }

    private IEnumerator Alert()
    {
        isCheckPlayer = true;
        Debug.Log("? (경계모드 들어감)");
        currentState = State.Alert;
        yield return new WaitForSeconds(alertDuration);
        currentState = Physics2D.Raycast(transform.position, player.position - transform.position, viewDistance, playerLayer) ? State.Chasing : State.Patrolling;
    }

    private void Chase()
    {
        Debug.Log("! (추적모드 들어감)");
        target = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        Vector2 scale = transform.position.x < target.x ? new Vector2(1, 1) : new Vector2(-1, 1);
        transform.localScale = scale;
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
        // Perform attack here (e.g. spawn a projectile or trigger an animation)'
        Debug.Log("Attack");
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        target.x = player.position.x;
        _lineRenderer.SetPosition(1, target);
        isAttack = true;
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        yield return new WaitForSeconds(0.2f);
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        yield return new WaitForSeconds(0.2f);

        if (_playerMove.IsParry)
        {
            //플레이어 패링 성공
            Debug.Log("플레이어 패링 성공");
        }
        else
        {
            //플레이어에 체력 깍기
            Debug.Log("플레이어 패링 실패");
        }

        isAttack = false;
        _lineRenderer.enabled = false;
        currentState = State.Chasing;
    }
}
