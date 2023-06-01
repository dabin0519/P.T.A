using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Patrolling,
    Alert,
    Chasing,
    Attacking
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform[] waypoints = null;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float alertDuration = 1f;
    [SerializeField] private float attackDelay = 2f;
    [SerializeField] private Transform shootPos;

    [SerializeField] private EnemySO _enemyData;

    public bool isAttack = false;

    private Transform player;
    private State currentState = State.Patrolling;
    private Vector2 target;
    private int currentWaypoint = 0;
    private bool isCheckPlayer;
    private bool isAttacking;

    private Animator _enemyAnim;
    private LineRenderer _lineRenderer;
    private PlayerSkill _playerSkill;


    private void Awake()
    {
        _enemyAnim = GetComponentInChildren<Animator>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _playerSkill = FindObjectOfType<PlayerSkill>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    private void Update()
    {
       /* if (_player.GetState() != PlayerState.Move || _player.GetState() == PlayerState.Die)
        {
            StopAllCoroutines();
            return;
        }*/

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
                Flip();
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
        _enemyAnim.SetTrigger("isAlert");
        currentState = State.Alert;
        yield return new WaitForSeconds(alertDuration);
        currentState = Physics2D.Raycast(transform.position, player.position - transform.position, viewDistance, playerLayer) ? State.Chasing : State.Patrolling;
    }

    private void Chase()
    {
        _enemyAnim.SetTrigger("isChase");
        target = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        Flip();
    }

    private void Flip()
    {
        Vector2 scale = transform.position.x < target.x ? new Vector2(1, 1) : new Vector2(-1, 1);
        transform.localScale = scale;
    }

    private void CheckForAttack()
    {
        if (Vector2.Distance(transform.position, player.position) < attackDistance && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }


    private IEnumerator Attack()
    {
        isAttacking = true;
        currentState = State.Attacking;
        transform.position = transform.position; // Stop moving
        yield return new WaitForSeconds(attackDelay);
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, shootPos.position);
        target.x = player.position.x;
        target.y = shootPos.position.y;
        _lineRenderer.SetPosition(1, target);
        isAttack = true;
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        yield return new WaitForSeconds(0.2f);
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        yield return new WaitForSeconds(0.2f);
        _enemyAnim.SetTrigger("isAttack");

        if (_playerSkill.ParryCheck())
        {
            //플레이어 패링 성공
            Debug.Log("플레이어 패링 성공");

            StartCoroutine(WaitCounter());
        }
        else
        {
            //플레이어에 체력 깍기
            Debug.Log("플레이어 패링 실패");
        }

        isAttack = false;
        isAttacking = false;
        _lineRenderer.enabled = false;
        currentState = State.Chasing;
    }

    private IEnumerator WaitCounter()
    {
        //yield return _playerSkill.ParryCheck == true;
        Vector3 startPos = player.position;
        startPos.y = shootPos.position.y;
        Vector3 endPos = new Vector3(15,0,0);

        while (true)
        {
            endPos.y = Random.Range(-11f, 5f);
            if (endPos.y < 0 && endPos.y >= -6.5f)  
                continue;
            break;
        }

        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
        yield return new WaitForSeconds(0.5f);
        _lineRenderer.enabled = false;
    }
}
