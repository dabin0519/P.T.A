using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab_su : MonoBehaviour
{
    
    [SerializeField,Tooltip("그랩의 범위")] GameObject grabRange;
    [SerializeField, Tooltip("그랩의 사정거리")] GameObject grabRangeEndPos;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject playerObj;
    [SerializeField] float coolTime;
    BoxCollider2D grabCollider;
    Player player;
    bool isGrab = false;
    bool isEnemy = false;
    Animator anim; // 플레이어 애니메이션

    private void Start() {
        anim = playerObj.GetComponent<Animator>();
        grabCollider = grabRange.GetComponent<BoxCollider2D>();
        player = GetComponentInParent<Player>();
        grabCollider.enabled = false;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && isGrab == false) {
            isGrab = true;
            StartCoroutine("ThrowGrab");
        }
    }

    private IEnumerator ThrowGrab() {
        grabCollider.enabled = true;
        player.SetState(PlayerState.Grab);
        anim.SetTrigger("Grab");
        grabRange.transform.DOMove(grabRangeEndPos.transform.position, 0.5f);
        if (isEnemy) yield break;
        yield return new WaitForSeconds(0.8f);
        grabRange.transform.position = startPoint.transform.position;
        grabCollider.enabled = false;
        //grabRange.transform.position = transform.position;
        player.SetState(PlayerState.Move);
        yield return new WaitForSeconds(coolTime);
        isGrab = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
        enemyAI.SetState(State.Grab);
        StartCoroutine("GetGrab", other);
        isEnemy = true;
        grabRange.transform.position = grabRangeEndPos.transform.position;
        grabCollider.enabled = false;
    }

    private IEnumerator GetGrab(Collider2D other) {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("IsGrab", true);
        float duration = 0.3f; // 이동하는 데 걸리는 시간
        Vector3 startPosition = grabRangeEndPos.transform.position;
        Vector3 endPosition = startPoint.transform.position;

        float elapsedTime = 0.0f;
        while (elapsedTime < duration) {
            grabRange.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            other.transform.position = grabRange.transform.position;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        grabRange.transform.position = endPosition; // 정확한 위치로 이동
        player.SetState(PlayerState.Move);
        anim.SetBool("IsGrab", false);
        isEnemy = false;
        yield return new WaitForSeconds(coolTime);
        isGrab = false;
    }
}
