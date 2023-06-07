using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private EnemySO _enemyData;

    public UnityEvent OnAttack;
    
    [HideInInspector] public bool _isCheckPlayer;

    private Transform _playerTrm;
    private State _currentState = State.Patroll;
    private Vector2 _target;
    private int _currentWaypoint = 0;

    private Animator _enemyAnim;
    private Player _player;


    private void Awake()
    {
        _enemyAnim = GetComponentInChildren<Animator>();
        _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
        _player = _playerTrm.Find("Visual").GetComponent<Player>();
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
        if (Vector2.Distance(transform.position, _playerTrm.position) < _enemyData.AttackDistance)
        {
            /*switch (_enemyData.EnemyMode) //SO로 지정 // unity event로 바꾸자
            {
                case EnemyEnum.Gun:
                    StartCoroutine(GunAttack());
                    break;
            }*/
            OnAttack?.Invoke();
            transform.position = transform.position;
            _currentState = State.Attack;
        }
    }

    public void SetState(State state)
    {
        _currentState = state;
    }
}
