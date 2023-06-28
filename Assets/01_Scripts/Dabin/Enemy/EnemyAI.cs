using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum State
{
    Patroll,
    Alert,
    Chase,
    Attack,
    Die,
    End
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform[] _waypoints = null;
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private EnemySO _enemyData;

    public UnityEvent OnAttack;
    
    [HideInInspector] public bool _isCheckPlayer;

    private State _currentState;
    private Transform _playerVisualTrm;
    private Vector2 _target;
    private int _currentWaypoint = 0;
    private Vector2 x = Vector2.left.normalized;

    private Animator _enemyAnim;
    private Player _player;
    private CapsuleCollider2D _collider;
    private EnemyAI _enemyAI;

    private void Awake()
    {
        _enemyAnim = GetComponentInChildren<Animator>();
        _player = _playerTrm.GetComponent<Player>();
        _playerVisualTrm = _playerTrm.Find("Visual").transform;
        _collider = GetComponent<CapsuleCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
        _enemyAnim.runtimeAnimatorController = _enemyData.Controller;
        _currentState = State.Patroll;

        if (_enemyData.IsPatrol)
        {
            _enemyAnim.SetBool("IsMove", true);
        }
    }

    private void Update()
    {
        if(_currentState == State.Die)
        {
            _collider.enabled = false;
            _enemyAnim.SetTrigger("IsDie");
            _currentState = State.End;
        }

        if (_player.GetState() == PlayerState.End || _currentState == State.End) // 플레이어가 죽었을땐 멈추기
        {
            _enemyAI.enabled = false;
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
        if (_waypoints.Length == 0) Debug.LogWarning("너 값 안넣었다.");

        _target = new Vector2(_waypoints[_currentWaypoint].position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, _target, _enemyData.Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _target) < 0.1f)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;

            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
            x *= -1;
        }
    }

    private void CheckForPlayer()
    {
        if (_isCheckPlayer)
        {
            _currentState = State.Chase;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, x, _enemyData.ViewDistance, _playerLayer);

        Debug.DrawRay(transform.position, _enemyData.ViewDistance * x, Color.red);


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
        _currentState = Physics2D.Raycast(transform.position, _playerVisualTrm.position - transform.position, _enemyData.ViewDistance, _playerLayer) ? State.Chase : State.Patroll;
    }

    private void Chase()
    {
        _enemyAnim.SetTrigger("isChase");
        Debug.Log("chase");
        _target = new Vector2(_playerVisualTrm.position.x, transform.position.y);
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
        if (Vector2.Distance(transform.position, _playerVisualTrm.position) < _enemyData.AttackDistance && CaculateForward())
        {
            OnAttack?.Invoke();
            _enemyAnim.SetTrigger("isShootWait");
            transform.position = transform.position;
            _currentState = State.Attack;
        }
    }

    public void SetState(State state)
    {
        _currentState = state;
    }

    private bool CaculateForward()
    {
        Vector2 a = (transform.position - _playerVisualTrm.position).normalized;
        Vector2 dir = Vector2.right.normalized;

        Debug.Log(Vector2.Dot(a, dir) > 0);
        return Vector2.Dot(a, dir) > 0;
    }
}
