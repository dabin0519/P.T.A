using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab_su : MonoBehaviour
{
    [SerializeField, Tooltip("그랩의 사정거리")] 
    private Transform _grabRangeEndPos;
    [SerializeField] private KeyCode _grabKey;
    [SerializeField] private Transform _startPoint; 
    [SerializeField] private Transform _playerObj;
    [SerializeField] private float _coolTime;

    private BoxCollider2D _grabCollider;
    private Player _player;
    private Animator _anim; // 플레이어 애니메이션
    private Transform _grabRange;

    private bool _isGrab = false;
    private bool _isEnemy = false;
    private bool _isGrabFailed = false;

    private void Start() {
        _grabRange = gameObject.transform;
        _anim = _playerObj.GetComponent<Animator>();
        _grabCollider = _grabRange.GetComponent<BoxCollider2D>();
        _player = GetComponentInParent<Player>();
        _grabCollider.enabled = false;
    }
    private void Update() {
        if (Input.GetKeyDown(_grabKey) && _isGrab == false) {
            _isGrab = true;
            StartCoroutine("ThrowGrab");
        }
    }

    private IEnumerator ThrowGrab() {
        _grabCollider.enabled = true;
        _player.SetState(PlayerState.Grab);
        _anim.SetTrigger("Grab");
        _grabRange.DOMove(_grabRangeEndPos.position, 0.5f);
        if (_isEnemy) yield break;
        yield return new WaitForSeconds(0.8f);
        _grabRange.position = _startPoint.position;
        _grabCollider.enabled = false;
        //grabRange.transform.position = transform.position;
        _player.SetState(PlayerState.Move);
        yield return new WaitForSeconds(_coolTime);
        _isGrab = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy") && _isGrabFailed == false) {
        EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
        enemyAI.SetState(State.Grab);
        StartCoroutine("GetGrab", other);
        _isEnemy = true;
        _grabRange.position = _grabRangeEndPos.position;
        _grabCollider.enabled = false;
        }

        else if (other.CompareTag("EnemyShiled") && _isGrabFailed == false) {
            StartCoroutine("GetGrab", other);
            _isEnemy = true;
            _grabRange.position = _grabRangeEndPos.position;
            _grabCollider.enabled = false;
        }

        else if (other.CompareTag("Shiled")) {
            _isGrabFailed = true;
            StopCoroutine(ThrowGrab());
            StartCoroutine(failedGrab());
        }
    }

    private IEnumerator failedGrab() {
        //_anim.SetTrigger("");
        print("Grab failed");
        yield return new WaitForSeconds(2);
        print("failedGrab coroutine stop");
        _isGrabFailed = true;
        yield break;
    }

    private IEnumerator GetGrab(Collider2D other) {
        yield return new WaitForSeconds(0.2f);
        _anim.SetBool("IsGrab", true);
        float duration = 0.3f; // 이동하는 데 걸리는 시간
        Vector3 startPosition = _grabRangeEndPos.position;
        Vector3 endPosition = _startPoint.position;

        float elapsedTime = 0.0f;
        while (elapsedTime < duration) {
            _grabRange.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            other.transform.position = _grabRange.position;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _grabRange.position = endPosition; // 정확한 위치로 이동
        _player.SetState(PlayerState.Move);
        _anim.SetBool("IsGrab", false);
        _isEnemy = false;
        yield return new WaitForSeconds(_coolTime);
        _isGrab = false;
    }
}
