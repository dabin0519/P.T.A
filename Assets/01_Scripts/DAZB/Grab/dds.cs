using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class dds : MonoBehaviour
{
    [SerializeField,Tooltip("그랩의 범위")] GameObject grabRange;
    [SerializeField, Tooltip("그랩의 사정거리")] GameObject grabRangeEndPos;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject playerObj;
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
            StartCoroutine("ThrowGarb");
        }
    }

    private IEnumerator ThrowGarb() {
        grabCollider.enabled = true;
        player.ChangeState(PlayerState.Grab);
        StartCoroutine("GrabObjThrow");
        anim.SetTrigger("GrabThrow");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        grabCollider.enabled = false;
        anim.SetBool("IsGrab", false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        player.ChangeState(PlayerState.Move);
        isGrab = false;
        isEnemy = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //anim.StopPlayback();
        StopCoroutine("GrabObjThrow");
        StartCoroutine("GetGrab", other);
        isEnemy = true;
        grabRange.transform.position = grabRangeEndPos.transform.position;
        grabCollider.enabled = false;
    }

    private IEnumerator GrabObjThrow() {
        grabRange.transform.DOMove(grabRangeEndPos.transform.position, anim.GetCurrentAnimatorStateInfo(0).length);
        grabRange.transform.position = startPoint.transform.position;
        yield return null;
    }

    private IEnumerator GetGrab(Collider2D other) {
        //grabRange.transform.position = grabRangeEndPos.transform.position;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("IsGrab", true);
        float duration = anim.GetCurrentAnimatorStateInfo(0).length -0.2f; // 이동하는 데 걸리는 시간
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
        yield return null;
    }

}