using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Patroll,
    Alert,
    Chase,
    Attack
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform[] _waypoints = null;
    [SerializeField] private Transform _shootPos;

    [SerializeField] private EnemySO _enemyData;

    public bool _isAttack = false;

    private Transform _playerTrm;
    private State _currentState = State.Patroll;
    private Vector2 _target; 
    private int _currentWaypoint = 0;
    private bool _isCheckPlayer;
    private bool _isAttacking;

    private Animator _enemyAnim;
    private LineRenderer _lineRenderer;
    private PlayerSkill _playerSkill;
    private Player _player;


    private void Awake()
    {
        _enemyAnim = GetComponentInChildren<Animator>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
        _player = _playerTrm.Find("Visual").GetComponent<Player>();
        _playerSkill = _playerTrm.Find("Visual").GetComponent<PlayerSkill>();
    }

    private void Start()
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    private void Update()
    {
        if (_player.GetState() == PlayerState.Die) // 플레이어가 죽었을땐 멈추기
        {
            StopAllCoroutines();
            return;
        }

        switch (_currentState)
        {
            case State.Patroll:
                Patrol();
                CheckForPlayer();
                break;
            case State.Alert:
                break;
            case State.Chase:
                Chase();
                CheckForAttack();
                break;
            case State.Attack:
                Flip();
                break;
        }
    }

    private void Patrol()
    {
        if (_waypoints.Length == 0) return;

        _target = new Vector2(_waypoints[_currentWaypoint].position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, _target, _enemyData.Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _target) < 0.1f)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;

            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    private void CheckForPlayer()
    {
        if (_isCheckPlayer)
        {
            _currentState = State.Chase;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _playerTrm.position - transform.position, _enemyData.ViewDistance, _playerLayer);

        if (hit && hit.collider.CompareTag("Player"))
        {
            _enemyAnim.SetTrigger("isAlert");
            StartCoroutine(Alert());
        }
    }

    private IEnumerator Alert()
    {
        _isCheckPlayer = true;
        _enemyAnim.SetTrigger("isAlert");
        _currentState = State.Alert;
        yield return new WaitForSeconds(_enemyData.AlretTime);
        _currentState = Physics2D.Raycast(transform.position, _playerTrm.position - transform.position, _enemyData.ViewDistance, _playerLayer) ? State.Chase : State.Patroll;
    }

    private void Chase()
    {
        _enemyAnim.SetTrigger("isChase");
        _target = new Vector2(_playerTrm.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, _target, _enemyData.Speed * Time.deltaTime);
        Flip();
    }

    private void Flip()
    {
        Vector2 scale = transform.position.x < _target.x ? new Vector2(1, 1) : new Vector2(-1, 1);
        transform.localScale = scale;
    }

    private void CheckForAttack()
    {
        if (Vector2.Distance(transform.position, _playerTrm.position) < _enemyData.AttackDistance && !_isAttacking)
        {
            switch (_enemyData.EnemyMode) //SO로 지정
            {
                case EnemyEnum.Gun:
                    StartCoroutine(GunAttack());
                    break;
            }
            _currentState = State.Attack; // Attack코드를 한번만 실행
        }
    }

    private IEnumerator GunAttack()
    {
        _isAttacking = true;
        transform.position = transform.position; // Stop moving
        yield return new WaitForSeconds(_enemyData.AttackCoolTime);
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, _shootPos.position);
        _target.x = _playerTrm.position.x;
        _target.y = _shootPos.position.y;
        _lineRenderer.SetPosition(1, _target);
        _isAttack = true;
        ChangeColor(Color.red);
        yield return new WaitForSeconds(0.2f);
        ChangeColor(Color.white);
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

        _isAttack = false;
        _isAttacking = false;
        _lineRenderer.enabled = false;
        _currentState = State.Chase;
    }

    private IEnumerator WaitCounter()
    {
        //yield return _playerSkill.ParryCheck == true;
        Vector3 startPos = _playerTrm.position;
        startPos.y = _shootPos.position.y;
        Vector3 endPos = new Vector3(15,0,0);

        while (true)
        {
            endPos.y = Random.Range(-11f, 5f);
            if (endPos.y < 0 && endPos.y >= -6.5f)  
                continue;
            break;
        }

        ChangeColor(Color.white);
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
        yield return new WaitForSeconds(0.5f);
        _lineRenderer.enabled = false;
    }

    private void ChangeColor(Color lineColor)
    {
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
    }
}
