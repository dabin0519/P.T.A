using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class EnemyCutScene : MonoBehaviour
{
    [SerializeField] private GameObject _doctor;
    [SerializeField] private SpriteRenderer _panel;
    [SerializeField] private EnemySO _enemyData;
    [SerializeField] private Animator _anim;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Player _player;

    public UnityEvent OnShoot;
    public UnityEvent OnDie;

    private Vector2 _target;
    private bool _caught;

    private void Start()
    {
        _target = _doctor.transform.position;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right.normalized, _enemyData.ViewDistance, _playerLayer);

        Debug.DrawRay(transform.position, _enemyData.ViewDistance * Vector2.right.normalized, Color.red);
        if (hit)
        {
            _anim.SetTrigger("Shoot");
            _caught = true;
        }
        else
        {
            if (Vector2.Distance(transform.position, _target) < 1f)
            {
                _anim.SetTrigger("Shoot");
            }
            else
            {
                _target.y = transform.position.y;
                transform.position = Vector2.MoveTowards(transform.position, _target, _enemyData.Speed * Time.deltaTime);
            }
        }
    }

    /*public void StartCor()
    {
        StartCoroutine(ShootCoroutine());
    }*/

    private IEnumerator ShootCoroutine()
    { 
        _panel.DOFade(1, 1);
        yield return new WaitForSeconds(2);
        if (_caught)
        {
            OnDie?.Invoke();
            _player.SetState(PlayerState.Die);
        }
        else
            OnShoot?.Invoke();
        _panel.DOFade(0, 1);
    }
}
